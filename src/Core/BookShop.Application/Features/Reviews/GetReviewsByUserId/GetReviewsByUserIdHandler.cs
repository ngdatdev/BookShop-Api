using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.GetReviewsByUserId;

/// <summary>
///     GetReviewsByUserId Handler
/// </summary>
public class GetReviewsByUserIdHandler
    : IFeatureHandler<GetReviewsByUserIdRequest, GetReviewsByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetReviewsByUserIdHandler(
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
    public async Task<GetReviewsByUserIdResponse> HandlerAsync(
        GetReviewsByUserIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all reviews by product id
        var reviews =
            await _unitOfWork.ReviewFeature.GetReviewsByUserIdRepository.FindAllReviewsByUserId(
                userId: request.UserId,
                pageIndex: request.Pageindex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the number of reviews by user id.
        var countReview =
            await _unitOfWork.ReviewFeature.GetReviewsByUserIdRepository.GetTotalNumberOfReviewByUserIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetReviewsByUserIdResponse()
        {
            StatusCode = GetReviewsByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Reviews = new()
                {
                    Contents = reviews.Select(review => new GetReviewsByUserIdResponse.Body.Review()
                    {
                        Comment = review.Comment,
                        ReviewId = review.Id,
                    }),
                    PageIndex = request.Pageindex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countReview / request.PageSize)
                }
            }
        };
    }
}
