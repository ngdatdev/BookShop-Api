using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     Interface for Query RestoreOrderDetailByIdRepository
/// </summary>
public partial interface IRestoreOrderDetailByIdRepository
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
