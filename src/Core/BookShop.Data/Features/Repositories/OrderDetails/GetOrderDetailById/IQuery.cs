using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;

/// <summary>
///     Interface for Query GetOrderDetailById Repository
/// </summary>
public partial interface IGetOrderDetailByIdRepository
{
    Task<bool> IsOrderDetailTemporarilyRemovedById(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    Task<bool> IsOrderDetailFoundByUserIdAndOrderDetailIdQueryAsync(
        Guid userId,
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    Task<OrderDetail> FindOrderDetailByIdQueryAsync(
        Guid orderDetailId,
        Guid userId,
        CancellationToken cancellationToken
    );
}
