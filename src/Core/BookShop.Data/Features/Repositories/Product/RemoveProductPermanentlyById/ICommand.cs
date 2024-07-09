using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///     Interface for Command RemoveProductPermanentlyById Repository
/// </summary>
public partial interface IRemoveProductPermanentlyByIdRepository
{
    Task<bool> RemoveProductPermanentlyByIdCommandAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
