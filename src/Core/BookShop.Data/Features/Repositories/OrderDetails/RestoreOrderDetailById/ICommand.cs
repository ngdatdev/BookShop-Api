using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     Interface for Command RestoreOrderDetailByIdRepository
/// </summary>
public partial interface IRestoreOrderDetailByIdRepository
{
    Task<bool> RestoreOrderDetailByIdCommandAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );
}
