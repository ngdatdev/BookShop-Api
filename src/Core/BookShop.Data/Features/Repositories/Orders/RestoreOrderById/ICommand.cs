using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Orders.RestoreOrderById;

/// <summary>
///     Interface for Command RestoreOrderByIdRepository
/// </summary>
public partial interface IRestoreOrderByIdRepository
{
    Task<bool> RestoreOrderByIdCommandAsync(Guid orderId, CancellationToken cancellationToken);
}
