using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Handler
/// </summary>
public class CreateProductHandler : IFeatureHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateProductHandler(
        IUnitOfWork unitOfWork,
        ICloudinaryStorageHandler cloudinaryStorageHandler,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _cloudinaryStorageHandler = cloudinaryStorageHandler;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<CreateProductResponse> HandlerAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        // Are categories id found.
        var areValidCategoriesId =
            await _unitOfWork.ProductFeature.CreateProductRepository.AreCategoriesFoundByIdsQueryAsync(
                categoriesId: request.CategoriesId,
                cancellationToken: cancellationToken
            );

        // Responds if one of categories id is not found.
        if (!areValidCategoriesId)
        {
            return new() { StatusCode = CreateProductResponseStatusCode.CATEGORY_ID_IS_NOT_VALID };
        }

        // Upload main url of product.
        var mainUrl = await _cloudinaryStorageHandler.UploadPhotoAsync(
            formFile: request.ImageUrl,
            cancellationToken: cancellationToken
        );

        // Responds if upload is fail.
        if (Equals(objA: mainUrl, objB: String.Empty))
        {
            return new() { StatusCode = CreateProductResponseStatusCode.MAIN_IMAGE_FILE_FAIL };
        }

        // Upload sub images url of product.
        var uploads = request.SubImages.Select(subImage =>
            _cloudinaryStorageHandler.UploadPhotoAsync(
                formFile: subImage,
                cancellationToken: cancellationToken
            )
        );

        var subUrls = await Task.WhenAll(uploads);

        // Responds if subUrls is fail.
        if (!Equals(objA: subUrls.Length, objB: request.SubImages.Count()))
        {
            await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: mainUrl);

            return new() { StatusCode = CreateProductResponseStatusCode.SUB_IMAGE_FILE_FAIL };
        }

        // Get userId from claim jwt token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Init mapper from productRequest to ProductEntity
        var product = InitProduct(
            createProductRequest: request,
            subImages: subUrls,
            mainImage: mainUrl,
            userId: Guid.Parse(input: userId)
        );

        // Create product to database
        var dbResult =
            await _unitOfWork.ProductFeature.CreateProductRepository.CreateProductCommandAsync(
                product: product,
                cancellationToken: cancellationToken
            );

        // Responds if dbResult is fail.
        if (dbResult)
        {
            try
            {
                await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: mainUrl);
                await Task.WhenAll(
                    subUrls.Select(subUrl =>
                        _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: subUrl)
                    )
                );
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }

            return new() { StatusCode = CreateProductResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Response successfully.
        return new CreateProductResponse()
        {
            StatusCode = CreateProductResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Data.Shared.Entities.Product InitProduct(
        CreateProductRequest createProductRequest,
        IEnumerable<string> subImages,
        string mainImage,
        Guid userId
    )
    {
        var productId = Guid.NewGuid();
        return new Data.Shared.Entities.Product()
        {
            Id = productId,
            FullName = createProductRequest.FullName,
            Description = createProductRequest.Description,
            Price = createProductRequest.Price,
            Discount = createProductRequest.Discount,
            ImageUrl = mainImage,
            NumberOfPage = createProductRequest.NumberOfPage,
            QuantityCurrent = createProductRequest.QuantityCurrent,
            QuantitySold = 0,
            Size = createProductRequest.Size,
            Author = createProductRequest.Author,
            Publisher = createProductRequest.Publisher,
            Languages = createProductRequest.Languages,
            Assets = subImages
                .Select(subImage => new Data.Shared.Entities.Asset()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = subImage,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                })
                .ToList(),
            ProductCategories = createProductRequest
                .CategoriesId.Select(categoryId => new ProductCategory()
                {
                    CategoryId = categoryId,
                    ProductId = productId
                })
                .ToList(),
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
        };
    }
}
