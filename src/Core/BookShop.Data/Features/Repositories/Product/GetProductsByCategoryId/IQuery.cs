using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;

namespace BookShop.Data.Features.Repositories.Product.GetProductsByCategoryId;

/// <summary>
///     Interface for Query IGetProductsByCategoryIdRepository Repository
/// </summary>
public partial interface IGetProductsByCategoryIdRepository
{
    Task<Category> FindCategoryByIdQueryAsync(Guid categoryId, CancellationToken cancellationToken);

    Task<IEnumerable<Shared.Entities.Product>> FindProductsByCategoryIdQueryAsync(
        Guid categoryId,
        FilterParameterQuery filterParameterQuery,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfProductsByCategoryIdQueryAsync(
        Guid categoryId,
        CancellationToken cancellationToken
    );
}
