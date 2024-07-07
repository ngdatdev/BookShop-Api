using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Reviews.UpdateReviewById;

/// <summary>
///     UpdateReviewById Response
/// </summary>
public class UpdateReviewByIdResponse : IFeatureResponse
{
    public UpdateReviewByIdResponseStatusCode StatusCode { get; init; }
}
