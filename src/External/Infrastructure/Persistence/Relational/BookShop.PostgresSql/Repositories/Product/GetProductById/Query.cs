using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductById;

/// <summary>
///    Implement of query IGetProductByIdRepository repository.
/// </summary>
internal partial class GetProductByIdRepository
{
    public Task<BookShop.Data.Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .Where(predicate: product =>
                product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(product => new BookShop.Data.Shared.Entities.Product()
            {
                FullName = product.FullName,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Author = product.Author,
                Assets = product.Assets.Select(asset => new BookShop.Data.Shared.Entities.Asset()
                {
                    ImageUrl = asset.ImageUrl,
                }),
                Discount = product.Discount,
                Languages = product.Languages,
                NumberOfPage = product.NumberOfPage,
                QuantityCurrent = product.QuantityCurrent,
                Price = product.Price,
                Publisher = product.Publisher,
                QuantitySold = product.QuantitySold,
                Reviews = product.Reviews.Select(
                    review => new BookShop.Data.Shared.Entities.Review()
                    {
                        Comment = review.Comment,
                        CreatedAt = CommonConstant.MIN_DATE_TIME,
                        UserId = review.UserId,
                    }
                ),
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsTemporarilyRemovedProductQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products.AnyAsync(
            predicate: product =>
                product.RemovedAt != CommonConstant.MIN_DATE_TIME
                && product.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            cancellationToken: cancellationToken
        );
    }
}
