using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.UpdatePaymentByWebHook;

/// <summary>
///    Implement of query IUpdatePaymentByWebHook repository.
/// </summary>
internal partial class UpdatePaymentByWebHookRepository
{
    public Task<Payment> FindPaymentByOrderIdQueryAsync(
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
