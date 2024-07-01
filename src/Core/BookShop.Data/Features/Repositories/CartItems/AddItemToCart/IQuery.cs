using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.CartItems.AddItemToCart;

/// <summary>
///     Interface for Query AddItemToCart Repository
/// </summary>
public partial interface IAddItemToCartRepository
{
    Task<Guid> FindCartIdByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );

    Task<CartItem> FindCartItemByProductIdAndCartIdQueryAsync(
        Guid productId,
        Guid cartId,
        CancellationToken cancellationToken
    );
}
