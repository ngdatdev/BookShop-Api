namespace BookShop.API.Controllers.Order.GetOrderById.Common;

/// <summary>
///     Represents the get GetOrderById state bag.
/// </summary>
internal sealed class GetOrderByIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
