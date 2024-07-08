namespace BookShop.API.Controllers.Review.GetReviewsByUserId.Common;

/// <summary>
///     Represents the get GetReviewsByUserId state bag.
/// </summary>
internal sealed class GetReviewsByUserIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
