namespace BookShop.API.Controllers.Review.GetReviewsByProductId.Common;

/// <summary>
///     Represents the get GetReviewsByProductId state bag.
/// </summary>
internal sealed class GetReviewsByProductIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
