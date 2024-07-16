using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     Interface for Command RestoreOrderStatusToConfirm Repository
/// </summary>
public partial interface IRestoreOrderStatusToConfirmRepository
{
    Task<bool> RestoreOrderStatusToConfirmCommandAsync(
        Guid orderDetailId,
        Guid newOrderStatusId,
        DateTime updatedAt,
        Guid updatedBy,
        CancellationToken cancellationToken
    );
}
