namespace BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.Common;

/// <summary>
///     Represents the get GetAllTemporarilyRemovedOrder state bag.
/// </summary>
internal sealed class GetAllTemporarilyRemovedOrderStateBag
{
    internal static int CacheDurationInSeconds { get; } = 60;
}
