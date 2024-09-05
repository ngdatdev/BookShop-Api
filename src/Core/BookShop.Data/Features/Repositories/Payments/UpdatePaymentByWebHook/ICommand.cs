using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Payments.UpdatePaymentByWebHook;

/// <summary>
///     Interface for Command UpdatePaymentByWebHook Repository
/// </summary>
public partial interface IUpdatePaymentByWebHookRepository
{
    Task<bool> UpdatePaymentByWebHookCommandAsync(
        Payment updatePayment,
        CancellationToken cancellationToken
    );
}
