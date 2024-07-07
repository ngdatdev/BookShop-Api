using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///    Implement of query IAddReviewWithUserAndProductId repository.
/// </summary>
internal partial class AddReviewWithUserAndProductIdRepository
{
    public Task<bool> IsProductFoundByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .AnyAsync(
                predicate: product => product.Id == productId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsProductTemporarilyRemovedQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .AnyAsync(
                predicate: product =>
                    product.Id == productId
                    && product.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && product.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
