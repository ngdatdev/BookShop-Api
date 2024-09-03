using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Payments.GetPaymentsByMethod;

/// <summary>
///     Interface for Query GetPaymentsByMethod Repository
/// </summary>
public partial interface IGetPaymentsByMethodRepository
{
    Task<IEnumerable<Shared.Entities.Payment>> FindAllPaymentsByMethodQueryAsync(
        PaymentMethod method,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfPayments(PaymentMethod method, CancellationToken cancellationToken);
}
