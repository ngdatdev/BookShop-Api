using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Carts.GetCartById;

/// <summary>
///    Implement of query IGetCartById repository.
/// </summary>
internal partial class GetCartByIdRepository
{
    public Task<Cart> FindCartByIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _carts
            .AsNoTracking()
            .Where(cart => cart.UserId == userId)
            .Select(selector: cart => new Cart()
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(cartItem => new CartItem()
                {
                    Product = new BookShop.Data.Shared.Entities.Product()
                    {
                        FullName = cartItem.Product.FullName,
                        ImageUrl = cartItem.Product.ImageUrl,
                        Size = cartItem.Product.Size,
                        Author = cartItem.Product.Author,
                        Discount = cartItem.Product.Discount,
                        Price = cartItem.Product.Price,
                    },
                    Quantity = cartItem.Quantity
                })
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
