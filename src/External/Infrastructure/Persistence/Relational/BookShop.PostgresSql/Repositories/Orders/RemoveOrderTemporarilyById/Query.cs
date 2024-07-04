using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RemoveOrderTemporarilyById;

/// <summary>
///    Implement of query IRemoveOrderTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveOrderTemporarilyByIdRepository
{
    public Task<bool> IsOrderFoundByIdQueryAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return _orders
            .AsNoTracking()
            .AnyAsync(
                predicate: order => order.Id == orderId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsOrderTemporarilyRemovedByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    )
    {
        return _orders
            .AsNoTracking()
            .AnyAsync(
                predicate: order =>
                    order.Id == orderId
                    && order.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && order.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
