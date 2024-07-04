using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     Interface for Query RemoveOrderTemporarilyByIdRepository
/// </summary>
public partial interface IRemoveOrderTemporarilyByIdRepository
{
    Task<bool> IsOrderFoundByIdQueryAsync(Guid orderId, CancellationToken cancellationToken);

    Task<bool> IsOrderTemporarilyRemovedByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    );
}
