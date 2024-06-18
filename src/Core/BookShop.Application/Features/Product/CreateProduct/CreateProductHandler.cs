using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;
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
        ICloudinaryStorageHandler cloudinaryStorageHandler
    )
    {
        _unitOfWork = unitOfWork;
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
    public async Task<CreateProductResponse> HandlerAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        var mainUrl = await _cloudinaryStorageHandler.UploadPhotoAsync(
            formFile: request.ImageUrl,
            cancellationToken: cancellationToken
        );

        if (Equals(objA: mainUrl, objB: default))
        {
            return new() { StatusCode = CreateProductResponseStatusCode.MAIN_IMAGE_FILE_FAIL };
        }

        var uploads = request.SubImages.Select(subImage =>
            _cloudinaryStorageHandler.UploadPhotoAsync(
                formFile: subImage,
                cancellationToken: cancellationToken
            )
        );

        var subUrls = await Task.WhenAll(uploads);

        if (Equals(objA: subUrls.Length, objB: request.SubImages.Count()))
        {
            return new() { StatusCode = CreateProductResponseStatusCode.SUB_IMAGE_FILE_FAIL };
        }

        var userId = _httpContextAccessor
            .HttpContext.User.FindFirst(type: JwtRegisteredClaimNames.Sub)
            .ToString();

        var product = InitProduct(
            createProductRequest: request,
            subImages: subUrls,
            mainImage: mainUrl,
            userId: Guid.Parse(input: userId)
        );

        var dbResult =
            await _unitOfWork.ProductFeature.CreateProductRepository.CreateProductCommandAsync(
                product: product,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
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
        return new Data.Shared.Entities.Product()
        {
            Id = Guid.NewGuid(),
            FullName = createProductRequest.FullName,
            Description = createProductRequest.Description,
            Price = createProductRequest.Price,
            Discount = createProductRequest.Discount,
            ImageUrl = mainImage,
            NumberOfPage = createProductRequest.NumberOfPage,
            QuantityCurrent = createProductRequest.QuantityCurrent,
            Author = createProductRequest.Author,
            Publisher = createProductRequest.Publisher,
            Languages = createProductRequest.Languages,
            Assets = subImages.Select(subImage => new Data.Shared.Entities.Asset()
            {
                Id = Guid.NewGuid(),
                ImageUrl = subImage,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                CreatedAt = DateTime.Now,
                CreatedBy = userId,
            }),
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            CreatedAt = DateTime.Now,
            CreatedBy = userId,
        };
    }
}
