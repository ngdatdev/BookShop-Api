using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     Interface for Query RestoreOrderStatusToConfirm Repository
/// </summary>
public partial interface IRestoreOrderStatusToConfirmRepository
{
    Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    Task<bool> IsOrderDetailTemporarilyRemovedByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    public Task<string> GetOrderStatusIdByOrderDetailIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    Task<Guid> GetOrderStatusIdByValueQueryAsync(
        string orderStatusValue,
        CancellationToken cancellationToken
    );
}
