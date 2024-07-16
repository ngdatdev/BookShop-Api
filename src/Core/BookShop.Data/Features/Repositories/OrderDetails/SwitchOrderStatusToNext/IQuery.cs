using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     Interface for Query SwitchOrderStatusToNext Repository
/// </summary>
public partial interface ISwitchOrderStatusToNextRepository
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
