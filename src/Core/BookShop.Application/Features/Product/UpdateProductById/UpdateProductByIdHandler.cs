using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Features.Product.CreateProduct;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Application.Features.Product.UpdateProductById;

/// <summary>
///     UpdateProductById Handler
/// </summary>
public class UpdateProductByIdHandler
    : IFeatureHandler<UpdateProductByIdRequest, UpdateProductByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;

    public UpdateProductByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ICloudinaryStorageHandler cloudinaryStorageHandler
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cloudinaryStorageHandler = cloudinaryStorageHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<UpdateProductByIdResponse> HandlerAsync(
        UpdateProductByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check id of categories.
        var categoryResult =
            await _unitOfWork.ProductFeature.UpdateProductByIdRepository.AreCategoriesFoundByIdsQueryAsync(
                categoriesId: request.CategoriesId,
                cancellationToken: cancellationToken
            );

        // Respond if categories id is not found.
        if (!categoryResult)
        {
            return new()
            {
                StatusCode = UpdateProductByIdResponseStatusCode.CATEGORY_ID_IS_NOT_FOUND,
            };
        }

        // Check id of product and get url images.
        var foundProduct =
            await _unitOfWork.ProductFeature.UpdateProductByIdRepository.FindProductByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Respond if product id is not found.
        if (Equals(objA: foundProduct, objB: default))
        {
            return new()
            {
                StatusCode = UpdateProductByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND
            };
        }

        string uploadMainUrl = string.Empty;
        // Upload main url to cloudinary.
        if (string.IsNullOrEmpty(request.OldMainUrl))
        {
            uploadMainUrl = await _cloudinaryStorageHandler.UploadPhotoAsync(
                formFile: request.MainUrl,
                cancellationToken: cancellationToken
            );
            // Respond if upload result of main url is fail.
            if (string.IsNullOrEmpty(uploadMainUrl))
            {
                return new()
                {
                    StatusCode = UpdateProductByIdResponseStatusCode.MAIN_IMAGE_FILE_FAIL,
                };
            }
        }

        // Handle upload sub url if it is not null.
        IEnumerable<string> subUrl = Enumerable.Empty<string>();
        if (!request.SubImages.IsNullOrEmpty())
        {
            subUrl = await Task.WhenAll(
                request.SubImages?.Select(subImage =>
                    _cloudinaryStorageHandler.UploadPhotoAsync(
                        formFile: subImage,
                        cancellationToken: cancellationToken
                    )
                )
            );

            // Respond if upload result of sub url is fail.
            if (!Equals(objA: subUrl.Count(), objB: request.SubImages.Count()))
            {
                await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: uploadMainUrl);
                return new()
                {
                    StatusCode = UpdateProductByIdResponseStatusCode.SUB_IMAGE_FILE_FAIL
                };
            }
        }

        // Get userId from claim jwt token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Mapping request to product entity.
        var updateProduct = MapperToProduct(
            updateProductByIdRequest: request,
            mainUrl: uploadMainUrl.IsNullOrEmpty() ? request.OldMainUrl : "",
            subUrls: subUrl.Concat(second: request.OldSubUrls ?? Enumerable.Empty<string>()),
            userId: Guid.Parse(input: userId),
            product: foundProduct
        );

        // Update product entity.
        var dbResult =
            await _unitOfWork.ProductFeature.UpdateProductByIdRepository.UpdateProductByIdCommandAsync(
                updateProduct: updateProduct,
                currentProduct: foundProduct,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            try
            {
                await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: uploadMainUrl);
                await Task.WhenAll(
                    subUrl.Select(subUrl =>
                        _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: subUrl)
                    )
                );
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            return new()
            {
                StatusCode = UpdateProductByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateProductByIdResponse()
        {
            StatusCode = UpdateProductByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    /// <summary>
    ///     Maps the update request to the product entity.
    /// </summary>
    /// <param name="updateProductByIdRequest">
    ///     The request containing the update details.
    ///     </param>
    /// <param name="mainUrl">
    ///     The URL of the main image.</param>
    /// <param name="subUrls">A collection of sub-image URLs.
    ///     </param>
    /// <param name="userId">
    ///     The ID of the user making the update.
    /// </param>
    /// <param name="product">
    ///     The current product entity.
    /// </param>
    /// <returns>
    ///     The updated product entity.
    /// </returns>
    private Data.Shared.Entities.Product MapperToProduct(
        UpdateProductByIdRequest updateProductByIdRequest,
        string mainUrl,
        IEnumerable<string> subUrls,
        Guid userId,
        Data.Shared.Entities.Product product
    )
    {
        var finalSubUrls = subUrls ?? Enumerable.Empty<string>();
        return new()
        {
            Id = updateProductByIdRequest.ProductId,
            FullName = updateProductByIdRequest.FullName,
            Description = updateProductByIdRequest.Description,
            Price = updateProductByIdRequest.Price,
            Discount = updateProductByIdRequest.Discount,
            Size = updateProductByIdRequest.Size,
            NumberOfPage = updateProductByIdRequest.NumberOfPage,
            QuantityCurrent = updateProductByIdRequest.QuantityCurrent,
            ImageUrl = mainUrl,
            Author = updateProductByIdRequest.Author,
            Publisher = updateProductByIdRequest.Publisher,
            Languages = updateProductByIdRequest.Languages,
            ProductCategories = updateProductByIdRequest
                .CategoriesId.Select(categoryId => new ProductCategory()
                {
                    CategoryId = categoryId,
                    ProductId = updateProductByIdRequest.ProductId
                })
                .ToList(),
            Assets = finalSubUrls
                .Select(subUrl => new Asset()
                {
                    ProductId = updateProductByIdRequest.ProductId,
                    ImageUrl = subUrl,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                })
                .ToList(),
            CreatedAt = product.CreatedAt,
            CreatedBy = product.CreatedBy,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = userId,
            RemovedAt = product.RemovedAt,
            RemovedBy = product.RemovedBy,
        };
    }
}
