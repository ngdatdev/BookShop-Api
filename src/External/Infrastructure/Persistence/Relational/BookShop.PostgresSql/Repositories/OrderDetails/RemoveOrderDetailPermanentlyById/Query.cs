using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///    Implement of query IRemoveOrderDetailPermanentlyById repository.
/// </summary>
internal partial class RemoveOrderDetailPermanentlyByIdRepository
{
    public Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: order => order.Id == orderDetailId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsOrderDetailTemporarilyRemovedByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: order =>
                    order.Id == orderDetailId
                    && order.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && order.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
