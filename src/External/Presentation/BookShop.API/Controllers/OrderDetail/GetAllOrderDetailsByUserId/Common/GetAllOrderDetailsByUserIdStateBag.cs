namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.Common;

/// <summary>
///     Represents the get GetAllOrderDetailsByUserId state bag.
/// </summary>
internal sealed class GetAllOrderDetailsByUserIdStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
