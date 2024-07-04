using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.CreateOrder;

/// <summary>
///    Implement of query ICreateOrder repository.
/// </summary>
internal partial class CreateOrderRepository
{
    public async Task<bool> CreateAddressCommandAsync(
        Address address,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _addresses.AddAsync(entity: address, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CreateOrderCommandAsync(
        Order order,
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
                //try
                //{
                await _orders.AddAsync(entity: order, cancellationToken: cancellationToken);

                await _cartItems
                    .Where(predicate: cartItem =>
                        order
                            .OrderDetails.Select(orderDetail => orderDetail.ProductId)
                            .Contains(cartItem.ProductId)
                    )
                    .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken: cancellationToken);
                dbTransactionResult = true;
                //}
                //catch
                //{
                //    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                //}
            });

        return dbTransactionResult;
    }
}
