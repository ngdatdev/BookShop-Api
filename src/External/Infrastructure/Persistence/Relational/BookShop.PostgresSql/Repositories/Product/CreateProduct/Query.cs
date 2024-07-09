using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.CreateProduct;

/// <summary>
///    Implement of query ICreateProducty repository.
/// </summary>
internal partial class CreateProductRepository
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
}
