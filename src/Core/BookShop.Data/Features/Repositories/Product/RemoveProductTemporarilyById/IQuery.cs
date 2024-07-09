using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.RemoveProductTemporarilyById;

/// <summary>
///     Interface for Query RemoveProductTemporarilyById Repository
/// </summary>
public partial interface IRemoveProductTemporarilyByIdRepository
{
    Task<bool> IsProductFoundByIdQueryAsync(Guid productId, CancellationToken cancellationToken);

    Task<bool> IsProductTemporarilyRemovedByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
