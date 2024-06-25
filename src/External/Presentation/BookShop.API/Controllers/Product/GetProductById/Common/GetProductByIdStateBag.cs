namespace BookShop.API.Controllers.Product.GetProductById.Common;

/// <summary>
///     Represents the get GetProductById state bag.
/// </summary>
internal sealed class GetProductByIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
