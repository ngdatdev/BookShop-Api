namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.Common;

/// <summary>
///     Represents the get GetOrderDetailsByOrderStatusId state bag.
/// </summary>
internal sealed class GetOrderDetailsByOrderStatusIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
