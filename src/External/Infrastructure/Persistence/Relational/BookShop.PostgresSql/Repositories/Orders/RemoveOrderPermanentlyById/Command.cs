using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RemoveOrderPermanentlyById;

/// <summary>
///    Implement of command IRemoveOrderPermanentlyByIdRepository.
/// </summary>
internal partial class RemoveOrderPermanentlyByIdRepository
{
    public async Task<bool> DeleteOrderPermanentlyByIdCommandAsync(
        Guid orderId,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _orderDetails
                        .Where(predicate: entity => entity.OrderId == orderId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _orders
                        .Where(predicate: entity => entity.Id == orderId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
