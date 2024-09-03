using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.GetAllPayments;

/// <summary>
///    Implement of query IGetAllPayments repository.
/// </summary>
internal partial class GetAllPaymentsRepository
{
    public async Task<IEnumerable<Payment>> FindAllPaymentsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _payments
            .AsNoTracking()
            .Where(predicate: payment =>
                payment.RemovedAt == CommonConstant.MIN_DATE_TIME
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

    public Task<int> GetTotalNumberOfPayments(CancellationToken cancellationToken)
    {
        return _payments.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }
}
