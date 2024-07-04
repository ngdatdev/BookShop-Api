using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     Interface for Query RemoveOrderPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveOrderPermanentlyByIdRepository
{
    Task<bool> IsOrderFoundByIdQueryAsync(Guid orderId, CancellationToken cancellationToken);

    Task<bool> IsOrderTemporarilyRemovedByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    );
}
