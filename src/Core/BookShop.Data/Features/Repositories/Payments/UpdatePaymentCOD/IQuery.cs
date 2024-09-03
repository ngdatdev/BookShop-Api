using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///     Interface for Query UpdatePaymentCOD Repository
/// </summary>
public partial interface IUpdatePaymentCODRepository
{
    Task<Shared.Entities.Payment> FindPaymentByIdQueryAsync(
        Guid orderId,
        CancellationToken cancellationToken
    );

    Task<bool> IsPaymentTemporarilyRemovedQueryAsync(
        Guid paymentId,
        CancellationToken cancellationToken
    );
}
