using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Reviews.UpdateReviewById;

/// <summary>
///     Interface for Query UpdateReviewById Repository
/// </summary>
public partial interface IUpdateReviewByIdRepository
{
    Task<bool> IsReviewFoundByIdQueryAsync(Guid reviewId, CancellationToken cancellationToken);
}
