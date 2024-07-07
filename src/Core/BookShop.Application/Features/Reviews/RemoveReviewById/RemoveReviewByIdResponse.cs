using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Reviews.RemoveReviewById;

/// <summary>
///     RemoveReviewById Response
/// </summary>
public class RemoveReviewByIdResponse : IFeatureResponse
{
    public RemoveReviewByIdResponseStatusCode StatusCode { get; init; }
}
