using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     Interface for Command RemoveOrderDetailTemporarilyById Repository
/// </summary>
public partial interface IRemoveOrderDetailTemporarilyByIdRepository
{
    Task<bool> RemoveOrderDetailTemporarilyByIdCommandAsync(
        Guid orderDetailId,
        DateTime removedAt,
        Guid removedBy,
        CancellationToken cancellationToken
    );
}
