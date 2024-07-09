using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Reviews.GetReviewsByUserId;

/// <summary>
///     Interface for Query GetReviewsByUserId Repository
/// </summary>
public partial interface IGetReviewsByUserIdRepository
{
    Task<IEnumerable<Review>> FindAllReviewsByUserId(
        Guid userId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfReviewByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
