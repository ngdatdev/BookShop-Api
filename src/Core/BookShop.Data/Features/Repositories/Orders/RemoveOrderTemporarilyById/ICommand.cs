using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     Interface for Command RemoveOrderTemporarilyByIdRepository
/// </summary>
public partial interface IRemoveOrderTemporarilyByIdRepository
{
    Task<bool> RemoveOrderTemporarilyByIdCommandAsync(
        Guid orderId,
        DateTime removedAt,
        Guid removedBy,
        CancellationToken cancellationToken
    );
}
