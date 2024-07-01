using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.CartItems.AddItemToCart;

/// <summary>
///     Interface for Command AddItemToCart Repository
/// </summary>
public partial interface IAddItemToCartRepository
{
    Task<bool> CreateCartItemCommandAsync(CartItem cartItems, CancellationToken cancellationToken);
}
