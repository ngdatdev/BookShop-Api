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
    Task<string> FindCartIdByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsProductFoundByIdQueryAsync(Guid productId, CancellationToken cancellationToken);
}
