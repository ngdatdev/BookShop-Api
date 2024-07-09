using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.UpdateProductById;

/// <summary>
///     Interface for Query UpdateProductById Repository
/// </summary>
public partial interface IUpdateProductByIdRepository
{
    Task<Shared.Entities.Product> FindProductByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    );

    Task<bool> AreCategoriesFoundByIdsQueryAsync(
        IEnumerable<Guid> categoriesId,
        CancellationToken cancellationToken
    );
}
