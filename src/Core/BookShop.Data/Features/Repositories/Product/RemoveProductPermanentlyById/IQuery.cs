using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///     Interface for Query RemoveProductPermanentlyById Repository
/// </summary>
public partial interface IRemoveProductPermanentlyByIdRepository
{
    Task<Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );

    Task<bool> IsProductTemporarilyRemovedByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
