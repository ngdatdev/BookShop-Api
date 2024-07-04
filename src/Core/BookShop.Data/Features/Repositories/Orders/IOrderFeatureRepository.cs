using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Features.Repositories.Orders.GetOrderById;

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

    /// <summary>
    ///     Gets get order by id feature repository.
    /// </summary>
    public IGetOrderByIdRepository GetOrderByIdRepository { get; }
}
