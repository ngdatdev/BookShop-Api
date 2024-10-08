using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RemoveOrderTemporarilyById;

/// <summary>
///    Implement of command IRemoveOrderTemporarilyById repository.
/// </summary>
internal partial class RemoveOrderTemporarilyByIdRepository
{
    public async Task<bool> RemoveOrderTemporarilyByIdCommandAsync(
        Guid orderId,
        DateTime removedAt,
        Guid removedBy,
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
                    _orders
                        .Where(predicate: order => order.Id == orderId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(order => order.RemovedAt, removedAt)
                                .SetProperty(order => order.RemovedBy, removedBy)
                        );

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    dbTransactionResult = false;
                }
            });

        return dbTransactionResult;
    }
}
