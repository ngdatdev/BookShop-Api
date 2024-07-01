using System;
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

    Task<bool> UpdateCartItemCommandAsync(
        Guid cartItemId,
        int quantity,
        CancellationToken cancellationToken
    );
}
