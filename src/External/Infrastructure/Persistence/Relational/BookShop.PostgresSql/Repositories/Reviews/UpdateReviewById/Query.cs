using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.UpdateReviewById;

/// <summary>
///    Implement of query IUpdateReviewById repository.
/// </summary>
internal partial class UpdateReviewByIdRepository
{
    public Task<bool> IsReviewFoundByIdQueryAsync(
        Guid reviewId,
        CancellationToken cancellationToken
    )
    {
        return _reviews
            .AsNoTracking()
            .AnyAsync(
                predicate: review => review.Id == reviewId,
                cancellationToken: cancellationToken
            );
    }
}
