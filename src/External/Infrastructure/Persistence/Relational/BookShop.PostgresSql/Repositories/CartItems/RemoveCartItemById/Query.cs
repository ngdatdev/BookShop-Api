using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///    Implement of query IRemoveCartItemById repository.
/// </summary>
internal partial class RemoveCartItemByIdRepository
{
    public Task<bool> IsCartItemFoundByIdQueryAsync(
        Guid cartItemId,
        CancellationToken cancellationToken
    )
    {
        return _cartItems
            .AsNoTracking()
            .AnyAsync(cartItem => cartItem.Id == cartItemId, cancellationToken: cancellationToken);
    }
}
