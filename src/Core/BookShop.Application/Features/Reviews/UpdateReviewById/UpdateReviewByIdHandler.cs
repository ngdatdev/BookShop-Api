using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Reviews.UpdateReviewById;

/// <summary>
///     UpdateReviewById Handler
/// </summary>
public class UpdateReviewByIdHandler
    : IFeatureHandler<UpdateReviewByIdRequest, UpdateReviewByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateReviewByIdHandler(
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
    public async Task<UpdateReviewByIdResponse> HandlerAsync(
        UpdateReviewByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is review id found.
        var isReviewFound =
            await _unitOfWork.ReviewFeature.UpdateReviewByIdRepository.IsReviewFoundByIdQueryAsync(
                reviewId: request.ReviewId,
                cancellationToken: cancellationToken
            );

        // Responds if review is not found.
        if (!isReviewFound)
        {
            return new() { StatusCode = UpdateReviewByIdResponseStatusCode.REVIEW_IS_NOT_FOUND };
        }

        // Get userId from claim jwt token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Create review to database
        var dbResult =
            await _unitOfWork.ReviewFeature.UpdateReviewByIdRepository.UpdateReviewByIdCommandAsync(
                userId: Guid.Parse(input: userId),
                reviewId: request.ReviewId,
                comment: request.Comment,
                cancellationToken: cancellationToken
            );

        // Responds if dbResult is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdateReviewByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateReviewByIdResponse()
        {
            StatusCode = UpdateReviewByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
