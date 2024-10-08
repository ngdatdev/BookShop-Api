using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.GetReviewsByProductId;

/// <summary>
///     GetReviewsByProductId Handler
/// </summary>
public class GetReviewsByProductIdHandler
    : IFeatureHandler<GetReviewsByProductIdRequest, GetReviewsByProductIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetReviewsByProductIdHandler(
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
    public async Task<GetReviewsByProductIdResponse> HandlerAsync(
        GetReviewsByProductIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all reviews by product id
        var reviews =
            await _unitOfWork.ReviewFeature.GetReviewsByProductIdRepository.FindAllReviewsByProductId(
                productId: request.ProductId,
                pageIndex: request.Pageindex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the number of review by product id.
        var countReview =
            await _unitOfWork.ReviewFeature.GetReviewsByProductIdRepository.GetTotalNumberOfReviewByProductIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetReviewsByProductIdResponse()
        {
            StatusCode = GetReviewsByProductIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Reviews = new()
                {
                    Contents = reviews.Select(
                        review => new GetReviewsByProductIdResponse.Body.Review()
                        {
                            Comment = review.Comment,
                            Fullname =
                                $"{review.UserDetail.FirstName} {review.UserDetail.LastName}",
                            ReviewId = review.Id,
                            UserId = review.UserId,
                        }
                    ),
                    PageIndex = request.Pageindex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countReview / request.PageSize)
                }
            }
        };
    }
}
