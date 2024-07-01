using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.CartItems.UpdateCartItemById;

/// <summary>
///     Interface for Query UpdateCartItemById Repository
/// </summary>
public partial interface IUpdateCartItemByIdRepository
{
    Task<CartItem> FindCartItemByIdQueryAsync(Guid cartItemId, CancellationToken cancellationToken);
}
