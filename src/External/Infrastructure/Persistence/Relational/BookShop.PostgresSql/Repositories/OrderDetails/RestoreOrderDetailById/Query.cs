using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RestoreOrderDetailById;

/// <summary>
///    Implement of query IRestoreOrderDetailByIdRepository repository.
/// </summary>
internal partial class RestoreOrderDetailByIdRepository
{
    public Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid ordetDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: order => order.Id == ordetDetailId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsOrderDetailTemporarilyRemovedByIdQueryAsync(
        Guid ordetDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: order =>
                    order.Id == ordetDetailId
                    && order.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && order.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
