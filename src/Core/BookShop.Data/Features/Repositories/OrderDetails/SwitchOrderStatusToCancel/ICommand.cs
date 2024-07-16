using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     Interface for Command SwitchOrderStatusToCancel Repository
/// </summary>
public partial interface ISwitchOrderStatusToCancelRepository
{
    Task<bool> SwitchOrderStatusToCancelCommandAsync(
        Guid orderDetailId,
        Guid newOrderStatusId,
        DateTime updatedAt,
        Guid updatedBy,
        CancellationToken cancellationToken
    );
}
