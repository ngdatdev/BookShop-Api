using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.RestoreOrderById;

/// <summary>
///     Interface for Query RestoreOrderByIdRepository
/// </summary>
public partial interface IRestoreOrderByIdRepository
{
    Task<bool> IsOrderFoundByIdQueryAsync(Guid orderId, CancellationToken cancellationToken);

    Task<bool> IsOrderTemporarilyRemovedByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    );
}
