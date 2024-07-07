using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.RemoveReviewById;

/// <summary>
///    Implement of query IRemoveReviewById repository.
/// </summary>
internal partial class RemoveReviewByIdRepository
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

    public Task<bool> IsProductTemporarilyRemovedQueryAsync(
        Guid reviewId,
        CancellationToken cancellationToken
    )
    {
        return _reviews
            .AsNoTracking()
            .AnyAsync(
                predicate: review =>
                    review.Id == reviewId
                    && review.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && review.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
