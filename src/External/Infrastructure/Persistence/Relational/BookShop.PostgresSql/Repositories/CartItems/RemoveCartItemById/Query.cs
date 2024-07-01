using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///    Implement of query IRemoveCartItemByIdRepository repository.
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
