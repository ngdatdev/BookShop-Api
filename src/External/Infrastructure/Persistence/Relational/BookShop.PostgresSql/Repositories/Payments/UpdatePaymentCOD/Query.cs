using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///    Implement of query IUpdatePaymentCOD repository.
/// </summary>
internal partial class UpdatePaymentCODRepository
{
    public Task<bool> IsPaymentTemporarilyRemovedQueryAsync(
        Guid paymentId,
        CancellationToken cancellationToken
    )
    {
        return _payments
            .AsNoTracking()
            .Where(predicate: payment =>
                payment.Id == paymentId
                && payment.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && payment.RemovedAt != CommonConstant.MIN_DATE_TIME
            )
            .AnyAsync(cancellationToken: cancellationToken);
    }

    public Task<Payment> FindPaymentByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    )
    {
        return _payments
            .AsNoTracking()
            .Where(predicate: payment => payment.OrderId == orderId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
