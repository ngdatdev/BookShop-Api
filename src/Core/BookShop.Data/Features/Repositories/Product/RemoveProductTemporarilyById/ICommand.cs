using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.RemoveProductTemporarilyById;

/// <summary>
///     Interface for Command RemoveProductTemporarilyByIdRepository
/// </summary>
public partial interface IRemoveProductTemporarilyByIdRepository
{
    Task<bool> RemoveProductTemporarilyByIdCommandAsync(
        Guid productId,
        DateTime removedAt,
        Guid removedBy,
        CancellationToken cancellationToken
    );
}
