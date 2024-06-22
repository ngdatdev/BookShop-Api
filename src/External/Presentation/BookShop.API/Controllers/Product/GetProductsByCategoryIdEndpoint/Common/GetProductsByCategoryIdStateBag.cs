namespace BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.Common;

/// <summary>
///     Represents the get GetProductsByCategoryId state bag.
/// </summary>
internal sealed class GetProductsByCategoryIdStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
