using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Payments.UpdatePaymentByWebHook;

/// <summary>
///     Interface for Query UpdatePaymentByWebHook Repository
/// </summary>
public partial interface IUpdatePaymentByWebHookRepository
{
    Task<Payment> FindPaymentByOrderIdQueryAsync(Guid orderId, CancellationToken cancellationToken);
}
