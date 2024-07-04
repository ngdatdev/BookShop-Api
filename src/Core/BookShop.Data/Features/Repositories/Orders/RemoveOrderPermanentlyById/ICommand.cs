using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     Interface for Command RemoveOrderPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveOrderPermanentlyByIdRepository
{
    Task<bool> DeleteOrderPermanentlyByIdCommandAsync(
        Guid orderId,
        CancellationToken cancellationToken
    );
}
