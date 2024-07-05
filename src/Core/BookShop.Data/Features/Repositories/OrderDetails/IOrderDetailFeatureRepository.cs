using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;

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
}
