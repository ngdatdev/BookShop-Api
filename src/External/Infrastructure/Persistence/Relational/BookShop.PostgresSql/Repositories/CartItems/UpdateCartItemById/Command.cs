using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.UpdateCartItemById;

/// <summary>
///    Implement of command IUpdateCartItemById repository.
/// </summary>
internal partial class UpdateCartItemByIdRepository
{
    public async Task<bool> UpdateCartItemCommandAsync(
        Guid cartItemId,
        int newQuantity,
        DateTime updateAt,
        Guid updateBy,
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
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(cartItem => cartItem.Quantity, newQuantity)
                                .SetProperty(cartItem => cartItem.UpdatedAt, updateAt)
                                .SetProperty(cartItem => cartItem.UpdatedBy, updateBy)
                        );

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

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
