using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.UpdateProductById;

/// <summary>
///    Implement of query IUpdateProductByIdRepository repository.
/// </summary>
internal partial class UpdateProductByIdRepository
{
    public async Task<bool> AreCategoriesFoundByIdsQueryAsync(
        IEnumerable<Guid> categoriesId,
        CancellationToken cancellationToken
    )
    {
        var categoriesFound = await _categories
            .AsNoTracking()
            .Where(predicate: category => categoriesId.Contains(category.Id))
            .CountAsync(cancellationToken: cancellationToken);

        return categoriesFound == categoriesId.Count();
    }

    public Task<BookShop.Data.Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        return _products
            .AsNoTracking()
            .Where(predicate: product => product.Id == productId)
            .Include(p => p.Assets)
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
