namespace BookShop.API.Controllers.Order.GetAllOrders.Common;

/// <summary>
///     Represents the get GetAllOrders state bag.
/// </summary>
internal sealed class GetAllOrdersStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
