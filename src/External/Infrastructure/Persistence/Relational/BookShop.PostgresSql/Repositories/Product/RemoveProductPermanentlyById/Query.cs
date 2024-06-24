using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///    Implement of query IRemoveProductPermanentlyByIdRepository repository.
/// </summary>
internal partial class RemoveProductPermanentlyByIdRepository
{
    public Task<BookShop.Data.Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .Where(predicate: product => product.Id == productId)
            .Select(selector: product => new BookShop.Data.Shared.Entities.Product()
            {
                ImageUrl = product.ImageUrl,
                Assets = product.Assets.Select(asset => new BookShop.Data.Shared.Entities.Asset()
                {
                    ImageUrl = asset.ImageUrl
                })
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
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
