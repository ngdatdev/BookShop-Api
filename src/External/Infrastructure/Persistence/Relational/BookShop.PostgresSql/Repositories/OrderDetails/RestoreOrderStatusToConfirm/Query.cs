using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///    Implement of query IRestoreOrderStatusToConfirm repository.
/// </summary>
internal partial class RestoreOrderStatusToConfirmRepository
{
    public Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
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
        return _orderDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: orderDetail =>
                    orderDetail.Id == orderDetailId
                    && orderDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && orderDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }

    public Task<string> GetOrderStatusIdByOrderDetailIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetails
            .AsNoTracking()
            .Where(predicate: orderDetail => orderDetail.Id == orderDetailId)
            .Select(selector: ordedrDetail => ordedrDetail.OrderStatus.FullName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<Guid> GetOrderStatusIdByValueQueryAsync(
        string orderStatusValue,
        CancellationToken cancellationToken
    )
    {
        return _orderStatus
            .AsNoTracking()
            .Where(predicate: entity =>
                EF.Functions.Collate(
                        entity.FullName,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(orderStatusValue)
            )
            .Select(orderStatus => orderStatus.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
