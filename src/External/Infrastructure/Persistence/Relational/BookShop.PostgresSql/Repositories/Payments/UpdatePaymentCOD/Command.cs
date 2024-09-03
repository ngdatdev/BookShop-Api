using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///    Implement of command IUpdatePaymentCOD repository.
/// </summary>
internal partial class UpdatePaymentCODRepository
{
    public async Task<bool> UpdatePaymentCODCommandAsync(
        Payment updatePayment,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;
        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _payments
                        .Where(predicate: entity => entity.Id == updatePayment.Id)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(entity => entity.UpdatedAt, updatePayment.PaymentDate)
                                .SetProperty(
                                    entity => entity.TransactionId,
                                    updatePayment.TransactionId
                                )
                                .SetProperty(
                                    entity => entity.PaymentDate,
                                    updatePayment.PaymentDate
                                )
                                .SetProperty(entity => entity.Status, updatePayment.Status)
                        );

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
    }
}
