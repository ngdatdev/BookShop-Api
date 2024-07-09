using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.GetReviewsByUserId;

/// <summary>
///    Implement of query IGetReviewsByUserId repository.
/// </summary>
internal partial class GetReviewsByUserIdRepository
{
    public async Task<IEnumerable<Review>> FindAllReviewsByUserId(
        Guid userId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _reviews
            .AsNoTracking()
            .Where(predicate: review =>
                review.ProductId == userId
                && review.RemovedAt == CommonConstant.MIN_DATE_TIME
                && review.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Skip(count: (pageIndex - 1) * pageSize)
            .Take(count: pageSize)
            .Select(review => new Review()
            {
                Id = review.Id,
                UserId = review.UserId,
                Comment = review.Comment,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> GetTotalNumberOfReviewByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _reviews
            .AsNoTracking()
            .Where(predicate: review =>
                review.UserId == userId
                && review.RemovedAt == CommonConstant.MIN_DATE_TIME
                && review.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }
}
