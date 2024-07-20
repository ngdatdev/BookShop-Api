using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.FilterAndPagination;

namespace BookShop.Data.Features.Repositories.Product.SearchProductsByKeyword;

/// <summary>
///     Interface for Query ISearchProductsByKeyword Repository
/// </summary>
public partial interface ISearchProductsByKeywordRepository
{
    Task<IEnumerable<Shared.Entities.Product>> FindProductsByKeywordQueryAsync(
        string keyword,
        double similarityThreshold,
        FilterParameterQuery filterParameterQuery,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfProductsByKeywordQueryAsync(
        string keyword,
        CancellationToken cancellationToken
    );
}
