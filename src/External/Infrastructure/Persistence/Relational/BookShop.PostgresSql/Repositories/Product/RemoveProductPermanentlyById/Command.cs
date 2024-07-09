using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///    Implement of command IRemoveProductPermanentlyById repository.
/// </summary>
internal partial class RemoveProductPermanentlyByIdRepository
{
    public async Task<bool> RemoveProductPermanentlyByIdCommandAsync(
        Guid productId,
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
                    await _assets
                        .Where(entity => entity.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _cartItems
                        .Where(entity => entity.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _orderItems
                        .Where(entity => entity.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _reviews
                        .Where(entity => entity.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _products
                        .Where(predicate: entity => entity.Id == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

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
