using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Payments.GetAllPayments;

/// <summary>
///     Interface for Query GetAllPayments Repository
/// </summary>
public partial interface IGetAllPaymentsRepository
{
    Task<IEnumerable<Shared.Entities.Payment>> FindAllPaymentsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfPayments(CancellationToken cancellationToken);
}
