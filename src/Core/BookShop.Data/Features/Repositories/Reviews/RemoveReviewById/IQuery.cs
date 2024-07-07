using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;

/// <summary>
///     Interface for Query RemoveReviewById Repository
/// </summary>
public partial interface IRemoveReviewByIdRepository
{
    Task<bool> IsReviewFoundByIdQueryAsync(Guid reviewId, CancellationToken cancellationToken);
}
