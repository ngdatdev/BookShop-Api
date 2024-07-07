using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     AddReviewWithUserAndProductId Handler
/// </summary>
public class AddReviewWithUserAndProductIdHandler
    : IFeatureHandler<AddReviewWithUserAndProductIdRequest, AddReviewWithUserAndProductIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddReviewWithUserAndProductIdHandler(
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
    public async Task<AddReviewWithUserAndProductIdResponse> HandlerAsync(
        AddReviewWithUserAndProductIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is product id found.
        var isProductFound =
            await _unitOfWork.ReviewFeature.AddReviewWithUserAndProductIdRepository.IsProductFoundByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is not found.
        if (!isProductFound)
        {
            return new()
            {
                StatusCode = AddReviewWithUserAndProductIdResponseStatusCode.PRODUCT_IS_NOT_FOUND
            };
        }

        // Is product temporarily removed.
        var isProductTemporarilyRemoved =
            await _unitOfWork.ReviewFeature.AddReviewWithUserAndProductIdRepository.IsProductTemporarilyRemovedQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product temporarily removed.
        if (isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    AddReviewWithUserAndProductIdResponseStatusCode.PRODUCT_IS_TEMPORARILY_REMOVED
            };
        }

        // Get userId from claim jwt token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Init new review information.
        var newReview = InitNewReview(request: request, userId: Guid.Parse(input: userId));

        // Create product to database
        var dbResult =
            await _unitOfWork.ReviewFeature.AddReviewWithUserAndProductIdRepository.AddReviewWithUserAndProductIdCommandAsync(
                newReview: newReview,
                cancellationToken: cancellationToken
            );

        // Responds if dbResult is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    AddReviewWithUserAndProductIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new AddReviewWithUserAndProductIdResponse()
        {
            StatusCode = AddReviewWithUserAndProductIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private static Review InitNewReview(AddReviewWithUserAndProductIdRequest request, Guid userId)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            ProductId = request.ProductId,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UserId = userId,
        };
    }
}
