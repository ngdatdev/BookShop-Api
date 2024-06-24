using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///    Implement of command IRemoveProductPermanentlyByIdRepository.
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
                        .Where(product => product.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _productCategories
                        .Where(product => product.ProductId == productId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _products
                        .Where(predicate: product => product.Id == productId)
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
