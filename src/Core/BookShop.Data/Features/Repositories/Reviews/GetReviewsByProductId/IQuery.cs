using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Reviews.GetReviewsByProductId;

/// <summary>
///     Interface for Query GetReviewsByProductId Repository
/// </summary>
public partial interface IGetReviewsByProductIdRepository
{
    Task<IEnumerable<Review>> FindAllReviewsByProductId(
        Guid productId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfReview(string authorName, CancellationToken cancellationToken);
}
