using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.GetReviewsByProductId;

/// <summary>
///    Implement of query IGetReviewsByProductId repository.
/// </summary>
internal partial class GetReviewsByProductIdRepository
{
    public async Task<IEnumerable<Review>> FindAllReviewsByProductId(
        Guid productId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _reviews
            .Where(predicate: review =>
                review.ProductId == productId
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
                UserDetail = new()
                {
                    FirstName = review.UserDetail.FirstName,
                    LastName = review.UserDetail.LastName,
                },
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
