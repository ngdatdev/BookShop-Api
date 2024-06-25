using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RestoreProductById;

/// <summary>
///    Implement of query IRestoreProductByIdRepository repository.
/// </summary>
internal partial class RestoreProductByIdRepository
{
    public Task<bool> IsProductFoundByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products.AnyAsync(
            predicate: product => product.Id == productId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsProductTemporarilyRemovedByIdQueryAsync(
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
