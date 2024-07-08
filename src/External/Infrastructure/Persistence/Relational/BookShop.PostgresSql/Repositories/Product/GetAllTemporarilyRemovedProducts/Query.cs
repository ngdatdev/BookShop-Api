using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///    Implement of query IGetAllTemporarilyRemovedProductsRepository repository.
/// </summary>
internal partial class GetAllTemporarilyRemovedProductsRepository
{
    public async Task<
        IEnumerable<BookShop.Data.Shared.Entities.Product>
    > FindTemporarilyRemovedProductsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _products
            .AsNoTracking()
            .Where(predicate: product =>
                product.RemovedAt != CommonConstant.MIN_DATE_TIME
                && product.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .OrderBy(keySelector: product => product.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(selector: product => new BookShop.Data.Shared.Entities.Product()
            {
                FullName = product.FullName,
                ImageUrl = product.ImageUrl,
                Author = product.Author,
                Discount = product.Discount,
                Price = product.Price,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> GetTotalNumberOfProducts(CancellationToken cancellationToken)
    {
        return _products.CountAsync(cancellationToken: cancellationToken);
    }
}
