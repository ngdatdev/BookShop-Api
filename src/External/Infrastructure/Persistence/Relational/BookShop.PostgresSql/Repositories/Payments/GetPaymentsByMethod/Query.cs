using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.GetPaymentsByMethod;

/// <summary>
///    Implement of query IGetPaymentsByMethod repository.
/// </summary>
internal partial class GetPaymentsByMethodRepository
{
    public async Task<IEnumerable<Payment>> FindAllPaymentsByMethodQueryAsync(
        PaymentMethod method,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _payments
            .AsNoTracking()
            .Where(predicate: payment =>
                payment.Method.Equals(method)
                && payment.RemovedAt == CommonConstant.MIN_DATE_TIME
                && payment.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .OrderBy(keySelector: payment => payment.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(selector: payment => new Payment()
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId,
                Status = payment.Status,
                Method = payment.Method,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> GetTotalNumberOfPayments(
        PaymentMethod method,
        CancellationToken cancellationToken
    )
    {
        return _payments
            .AsNoTracking()
            .Where(entity => entity.Method.Equals(method))
            .CountAsync(cancellationToken: cancellationToken);
    }
}
