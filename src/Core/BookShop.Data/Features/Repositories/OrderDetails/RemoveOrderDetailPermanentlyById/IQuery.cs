using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     Interface for Query RemoveOrderDetailPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveOrderDetailPermanentlyByIdRepository
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
