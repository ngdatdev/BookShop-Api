using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductTemporarilyById;

/// <summary>
///    Implement of command IRemoveProductTemporarilyById repository.
/// </summary>
internal partial class RemoveProductTemporarilyByIdRepository
{
    public async Task<bool> RemoveProductTemporarilyByIdCommandAsync(
        Guid productId,
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
                    _products
                        .Where(predicate: product => product.Id == productId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(product => product.RemovedAt, removedAt)
                                .SetProperty(product => product.RemovedBy, removedBy)
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
