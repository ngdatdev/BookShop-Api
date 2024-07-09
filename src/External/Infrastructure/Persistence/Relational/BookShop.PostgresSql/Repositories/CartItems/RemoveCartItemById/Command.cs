using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///    Implement of command IRemoveCartItemById repository.
/// </summary>
internal partial class RemoveCartItemByIdRepository
{
    public async Task<bool> DeleteCartItemByIdCommandAsync(
        Guid cartItemId,
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
                    await _cartItems
                        .Where(predicate: cartItem => cartItem.Id == cartItemId)
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
