using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     Interface for Query AddReviewWithUserAndProductId Repository
/// </summary>
public partial interface IAddReviewWithUserAndProductIdRepository
{
    Task<bool> IsProductFoundByIdQueryAsync(Guid productId, CancellationToken cancellationToken);

    Task<bool> IsProductTemporarilyRemovedQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );
}
