using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.RestoreProductById;

/// <summary>
///     Interface for Query =RestoreProductById Repository
/// </summary>
public partial interface IRestoreProductByIdRepository
{
    Task<bool> IsProductFoundByIdQueryAsync(Guid productId, CancellationToken cancellationToken);

    Task<bool> IsProductTemporarilyRemovedByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
