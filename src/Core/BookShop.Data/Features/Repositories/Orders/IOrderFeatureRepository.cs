using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Features.Repositories.Orders.GetAllOrders;
using BookShop.Data.Features.Repositories.Orders.GetOrderById;
using BookShop.Data.Features.Repositories.Orders.GetOrdersByUserId;
using BookShop.Data.Features.Repositories.Orders.RemoveOrderTemporarilyById;

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

    /// <summary>
    ///     Gets get orders by user id feature repository.
    /// </summary>
    public IGetOrdersByUserIdRepository GetOrdersByUserIdRepository { get; }

    /// <summary>
    ///     Gets get all orders feature repository.
    /// </summary>
    public IGetAllOrdersRepository GetAllOrdersRepository { get; }

    /// <summary>
    ///     Gets remove order temporarily by id feature repository.
    /// </summary>
    public IRemoveOrderTemporarilyByIdRepository RemoveOrderTemporarilyByIdRepository { get; }
}
