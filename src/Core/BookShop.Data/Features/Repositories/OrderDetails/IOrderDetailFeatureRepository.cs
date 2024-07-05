using BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

namespace BookShop.Data.Features.Repositories.OrderDetails;

/// <summary>
///     Interface for order detail repository manager.
/// </summary>
public interface IOrderDetailFeatureRepository
{
    /// <summary>
    ///     Gets get order detail by id feature repository.
    /// </summary>
    public IGetOrderDetailByIdRepository GetOrderDetailByIdRepository { get; }

    /// <summary>
    ///     Gets get order details by order status id feature repository.
    /// </summary>
    public IGetOrderDetailsByOrderStatusIdRepository GetOrderDetailsByOrderStatusIdRepository { get; }

    /// <summary>
    ///     Gets get all order details by user id feature repository.
    /// </summary>
    public IGetAllOrderDetailsByUserIdRepository GetAllOrderDetailsByUserIdRepository { get; }

    /// <summary>
    ///     Gets get all temporarily removed order details feature repository.
    /// </summary>
    public IGetAllTemporarilyRemovedOrderDetailsRepository GetAllTemporarilyRemovedOrderDetailsRepository { get; }
}
