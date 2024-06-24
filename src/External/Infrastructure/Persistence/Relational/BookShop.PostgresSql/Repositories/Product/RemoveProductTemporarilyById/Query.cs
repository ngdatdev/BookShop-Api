using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductTemporarilyById;

/// <summary>
///    Implement of query IRemoveProductTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveProductTemporarilyByIdRepository
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
