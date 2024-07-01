using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.CartItems.UpdateCartItemById;

/// <summary>
///     Interface for Command UpdateCartItemById Repository
/// </summary>
public partial interface IUpdateCartItemByIdRepository
{
    Task<bool> UpdateCartItemCommandAsync(
        Guid cartItemId,
        int quantity,
        DateTime updateAt,
        Guid updateBy,
        CancellationToken cancellationToken
    );
}
