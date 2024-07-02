using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Carts.ClearCart;

/// <summary>
///     Interface for Query ClearCart Repository
/// </summary>
public partial interface IClearCartRepository
{
    Task<Guid> FindCartIdByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);
}
