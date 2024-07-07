using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     Interface for Query AddReviewWithUserAndProductId Repository
/// </summary>
public partial interface IAddReviewWithUserAndProductIdRepository
{
    Task<bool> AddReviewWithUserAndProductIdCommandAsync(
        Review newReview,
        CancellationToken cancellationToken
    );
}
