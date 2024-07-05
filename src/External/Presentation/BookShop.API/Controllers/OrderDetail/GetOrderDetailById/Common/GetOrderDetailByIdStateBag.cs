namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById.Common;

/// <summary>
///     Represents the get GetOrderDetailById state bag.
/// </summary>
internal sealed class GetOrderDetailByIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
