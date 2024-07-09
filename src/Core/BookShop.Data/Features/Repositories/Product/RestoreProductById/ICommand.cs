using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.RestoreProductById;

/// <summary>
///     Interface for Command RestoreProductById Repository
/// </summary>
public partial interface IRestoreProductByIdRepository
{
    Task<bool> RestoreProductByIdCommandAsync(Guid productId, CancellationToken cancellationToken);
}
