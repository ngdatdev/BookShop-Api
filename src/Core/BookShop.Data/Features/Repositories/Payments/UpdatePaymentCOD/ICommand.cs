using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///     Interface for Command UpdatePaymentCOD Repository
/// </summary>
public partial interface IUpdatePaymentCODRepository
{
    Task<bool> UpdatePaymentCODCommandAsync(
        Payment updatePayment,
        CancellationToken cancellationToken
    );
}
