using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///     Interface for Query RemoveCartItemById Repository
/// </summary>
public partial interface IRemoveCartItemByIdRepository
{
    Task<bool> IsCartItemFoundByIdQueryAsync(Guid cartItemId, CancellationToken cancellationToken);
}
