using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of command IAddItemToCartRepository.
/// </summary>
internal partial class AddItemToCartRepository
{
    public async Task<bool> CreateCartItemCommandAsync(
        CartItem cartItems,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _cartItems.AddAsync(entity: cartItems);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateCartItemCommandAsync(
        Guid cartItemId,
        int newQuantity,
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
                            builder.SetProperty(cartItem => cartItem.Quantity, newQuantity)
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
