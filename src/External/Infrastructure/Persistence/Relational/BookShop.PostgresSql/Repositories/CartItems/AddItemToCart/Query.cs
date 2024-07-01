using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of query IAddItemToCartRepository repository.
/// </summary>
internal partial class AddItemToCartRepository
{
    public Task<Guid> FindCartIdByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _carts
            .AsNoTracking()
            .Where(predicate: cart => cart.UserId == userId)
            .Select(selector: cart => cart.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<BookShop.Data.Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .Where(predicate: product => product.Id == productId)
            .Select(product => new BookShop.Data.Shared.Entities.Product()
            {
                Price = product.Price,
                QuantityCurrent = product.QuantityCurrent,
                Discount = product.Discount,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<CartItem> FindCartItemByProductIdAndCartIdQueryAsync(
        Guid productId,
        Guid cartId,
        CancellationToken cancellationToken
    )
    {
        return _cartItems
            .AsNoTracking()
            .Where(predicate: cartItem =>
                cartItem.ProductId == productId && cartItem.CartId == cartId
            )
            .Select(selector: cartItem => new CartItem()
            {
                Id = cartItem.Id,
                Quantity = cartItem.Quantity,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
