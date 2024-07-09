using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///     Interface for Query GetAllTemporarilyRemovedProducts Repository.
/// </summary>
public partial interface IGetAllTemporarilyRemovedProductsRepository
{
    Task<IEnumerable<Shared.Entities.Product>> FindTemporarilyRemovedProductsQueryAsync(
        int pageIndex,
        int PageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfTemporarilyRemovedProductsQueryAsync(
        CancellationToken cancellationToken
    );
}
