namespace BookShop.API.Controllers.Product.GetAllProductsEndpoint.Common;

/// <summary>
///     Represents the get all products state bag.
/// </summary>
internal sealed class GetAllProductsStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
}
