using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     AddReviewWithUserAndProductId Response
/// </summary>
public class AddReviewWithUserAndProductIdResponse : IFeatureResponse
{
    public AddReviewWithUserAndProductIdResponseStatusCode StatusCode { get; init; }
}
