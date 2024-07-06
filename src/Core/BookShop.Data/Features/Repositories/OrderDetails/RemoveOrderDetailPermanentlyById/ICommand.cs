using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     Interface for Command RemoveOrderDetailPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveOrderDetailPermanentlyByIdRepository
{
    Task<bool> DeleteOrderDetailPermanentlyByIdCommandAsync(
        Guid orderDetailId,
        CancellationToken cancellationToken
    );
}
