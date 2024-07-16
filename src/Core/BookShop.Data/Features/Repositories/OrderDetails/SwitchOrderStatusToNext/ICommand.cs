using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     Interface for Command SwitchOrderStatusToNext Repository
/// </summary>
public partial interface ISwitchOrderStatusToNextRepository
{
    Task<bool> SwitchOrderStatusToNextCommandAsync(
        Guid orderDetailId,
        Guid newOrderStatusId,
        DateTime updatedAt,
        Guid updatedBy,
        CancellationToken cancellationToken
    );
}
