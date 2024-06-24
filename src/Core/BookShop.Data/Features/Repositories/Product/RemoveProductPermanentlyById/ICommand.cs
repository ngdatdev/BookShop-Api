using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///     Interface for Command RemoveProductPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveProductPermanentlyByIdRepository
{
    Task<bool> RemoveProductPermanentlyByIdCommandAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
