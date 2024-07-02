using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Carts.ClearCart;

/// <summary>
///    Implement of query IClearCart repository.
/// </summary>
internal partial class ClearCartRepository
{
    public async Task<bool> ClearCartCommandAsync(Guid cartId, CancellationToken cancellationToken)
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
                        .Where(predicate: cartItem => cartItem.CartId == cartId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _carts
                        .Where(predicate: cart => cart.Id == cartId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder.SetProperty(cart => cart.UpdatedAt, DateTime.UtcNow)
                        );
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
