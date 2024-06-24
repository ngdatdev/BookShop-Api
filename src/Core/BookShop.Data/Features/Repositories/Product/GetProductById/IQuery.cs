using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.GetProductById;

/// <summary>
///     Interface for Query GetProductByIdRepository.
/// </summary>
public partial interface IGetProductByIdRepository
{
    Task<Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );

    Task<bool> IsTemporarilyRemovedProductQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
