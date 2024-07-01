namespace BookShop.API.Controllers.Cart.GetCartByUserId.Common;

/// <summary>
///     Represents the GetCartByUserId state bag.
/// </summary>
internal sealed class GetCartByUserIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
