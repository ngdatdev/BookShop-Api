using System;
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

namespace BookShop.Application.Features.Reviews.RemoveReviewById;

/// <summary>
///     RemoveReviewById Handler
/// </summary>
public class RemoveReviewByIdHandler
    : IFeatureHandler<RemoveReviewByIdRequest, RemoveReviewByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveReviewByIdHandler(
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
    public async Task<RemoveReviewByIdResponse> HandlerAsync(
        RemoveReviewByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is review id found.
        var isReviewFound =
            await _unitOfWork.ReviewFeature.RemoveReviewByIdRepository.IsReviewFoundByIdQueryAsync(
                reviewId: request.ReviewId,
                cancellationToken: cancellationToken
            );

        // Responds if review is not found.
        if (!isReviewFound)
        {
            return new() { StatusCode = RemoveReviewByIdResponseStatusCode.REVIEW_IS_NOT_FOUND };
        }

        // Get userId from claim jwt token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Create product to database
        var dbResult =
            await _unitOfWork.ReviewFeature.RemoveReviewByIdRepository.RemoveReviewByIdCommandAsync(
                reviewId: request.ReviewId,
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if dbResult is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveReviewByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new RemoveReviewByIdResponse()
        {
            StatusCode = RemoveReviewByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
