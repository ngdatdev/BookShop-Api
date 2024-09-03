using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///     Interface for Command UpdatePaymentCOD Repository
/// </summary>
public partial interface IUpdatePaymentCODRepository
{
    Task<bool> UpdatePaymentCODCommandAsync(
        Shared.Entities.Payment updatePayment,
        CancellationToken cancellationToken
    );
}
