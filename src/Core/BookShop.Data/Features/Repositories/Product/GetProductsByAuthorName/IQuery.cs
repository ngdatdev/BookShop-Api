using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;

namespace BookShop.Data.Features.Repositories.Product.GetProductsByAuthorName;

/// <summary>
///     Interface for Query IGetProductsByAuthorNameRepository Repository
/// </summary>
public partial interface IGetProductsByAuthorNameRepository
{
    Task<IEnumerable<Shared.Entities.Product>> FindProductsByAuthorNameQueryAsync(
        string authorName,
        FilterParameterQuery filterParameterQuery,
        CancellationToken cancellationToken
    );
}
