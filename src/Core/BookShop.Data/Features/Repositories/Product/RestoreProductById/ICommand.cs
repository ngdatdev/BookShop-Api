using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.RestoreProductById;

/// <summary>
///     Interface for Command RestoreProductByIdRepository
/// </summary>
public partial interface IRestoreProductByIdRepository
{
    Task<bool> RestoreProductByIdCommandAsync(Guid productId, CancellationToken cancellationToken);
}
