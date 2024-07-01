using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///     Interface for Command RemoveCartItemById Repository
/// </summary>
public partial interface IRemoveCartItemByIdRepository
{
    Task<bool> DeleteCartItemByIdCommandAsync(Guid cartItemId, CancellationToken cancellationToken);
}
