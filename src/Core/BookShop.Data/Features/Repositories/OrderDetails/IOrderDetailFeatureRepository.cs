using BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;
using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;
using BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderStatusToConfirm;
using BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToCancel;
using BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToNext;

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

    /// <summary>
    ///     Gets remove order detail temporarily by id all feature repository.
    /// </summary>
    public IRemoveOrderDetailTemporarilyByIdRepository RemoveOrderDetailTemporarilyByIdRepository { get; }

    /// <summary>
    ///     Gets remove order detail permanently by id all feature repository.
    /// </summary>
    public IRemoveOrderDetailPermanentlyByIdRepository RemoveOrderDetailPermanentlyByIdRepository { get; }

    /// <summary>
    ///     Gets restore order detail by id all feature repository.
    /// </summary>
    public IRestoreOrderDetailByIdRepository RestoreOrderDetailByIdRepository { get; }

    /// <summary>
    ///     Gets switch order status to next feature repository.
    /// </summary>
    public ISwitchOrderStatusToNextRepository SwitchOrderStatusToNextByIdRepository { get; }

    /// <summary>
    ///     Gets switch order status to cancel feature repository.
    /// </summary>
    public ISwitchOrderStatusToCancelRepository SwitchOrderStatusToCancelRepository { get; }

    /// <summary>
    ///     Gets restore order status to default confirm feature repository.
    /// </summary>
    public IRestoreOrderStatusToConfirmRepository RestoreOrderStatusToConfirmRepository { get; }
}
