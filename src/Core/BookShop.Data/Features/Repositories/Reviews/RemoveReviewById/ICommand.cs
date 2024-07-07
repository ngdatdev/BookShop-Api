using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;

/// <summary>
///     Interface for Query RemoveReviewById Repository
/// </summary>
public partial interface IRemoveReviewByIdRepository
{
    Task<bool> RemoveReviewByIdCommandAsync(
        Guid reviewId,
        Guid userId,
        CancellationToken cancellationToken
    );
}
