using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.CreateProduct;

/// <summary>
///     Interface for Query CreateProductRepository Repository
/// </summary>
public partial interface ICreateProductRepository
{
    Task<bool> AreCategoriesFoundByIdsQueryAsync(
        IEnumerable<Guid> categoriesId,
        CancellationToken cancellationToken
    );
}
