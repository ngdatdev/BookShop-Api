using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     Interface for Query RemoveOrderDetailTemporarilyByIdRepository
/// </summary>
public partial interface IRemoveOrderDetailTemporarilyByIdRepository
{
    Task<bool> IsOrderDetailFoundByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );

    Task<bool> IsOrderDetailTemporarilyRemovedByIdQueryAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );
}
