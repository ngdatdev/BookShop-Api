using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///    Implement of query IRemoveOrderDetailTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveOrderDetailTemporarilyByIdRepository
{
    public Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orders
            .AsNoTracking()
            .AnyAsync(
                predicate: orderDetail => orderDetail.Id == orderDetailId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsOrderDetailTemporarilyRemovedByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orders
            .AsNoTracking()
            .AnyAsync(
                predicate: orderDetail =>
                    orderDetail.Id == orderDetailId
                    && orderDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && orderDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
