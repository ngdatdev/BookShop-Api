using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Reviews.UpdateReviewById;

/// <summary>
///     Interface for Query UpdateReviewById Repository
/// </summary>
public partial interface IUpdateReviewByIdRepository
{
    Task<bool> UpdateReviewByIdCommandAsync(
        Guid reviewId,
        string comment,
        Guid userId,
        CancellationToken cancellationToken
    );
}
