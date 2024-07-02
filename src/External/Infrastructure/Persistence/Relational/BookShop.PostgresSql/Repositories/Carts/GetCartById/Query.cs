using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
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
            .Where(predicate: cart => cart.UserId == userId)
            .Select(selector: cart => new Cart()
            {
                Id = cart.Id,
                CartItems = cart
                    .CartItems.Where(cartItem =>
                        cartItem.Product.RemovedAt == CommonConstant.MIN_DATE_TIME
                        && cartItem.Product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                    )
                    .Select(cartItem => new CartItem()
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
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
