using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.UpdateCartItemById;

/// <summary>
///    Implement of query IUpdateCartItemByIdRepository repository.
/// </summary>
internal partial class UpdateCartItemByIdRepository
{
    public Task<CartItem> FindCartItemByIdQueryAsync(
        Guid cartItemId,
        CancellationToken cancellationToken
    )
    {
        return _cartItems
            .AsNoTracking()
            .Where(predicate: cartItem => cartItem.Id == cartItemId)
            .Select(selector: cartItem => new CartItem()
            {
                Quantity = cartItem.Quantity,
                Product = new BookShop.Data.Shared.Entities.Product()
                {
                    QuantityCurrent = cartItem.Product.QuantityCurrent
                }
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
