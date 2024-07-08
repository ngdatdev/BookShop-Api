using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Product.GetAllProducts;

/// <summary>
///     Interface for Query RefreshTokenRepository Repository
/// </summary>
public partial interface IGetAllProductsRepository
{
    Task<IEnumerable<Shared.Entities.Product>> GetAllProductsQueryAsync(
        int pageIndex,
        int PageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfProducts(CancellationToken cancellationToken);
}
