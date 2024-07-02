using BookShop.Data.Features.Repositories.Orders.CreateOrder;

namespace BookShop.Data.Features.Repositories.Orders;

/// <summary>
///     Interface for order repository manager.
/// </summary>
public interface IOrderFeatureRepository
{
    /// <summary>
    ///     Gets create order feature repository.
    /// </summary>
    public ICreateOrderRepository CreateOrderRepository { get; }
}
