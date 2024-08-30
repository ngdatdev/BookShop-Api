using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;
using BookShop.PostgresSql.Repositories.Product.SearchProductsByKeyword.QueryMapper;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.SearchProductsByKeyword;

/// <summary>
///    Implement of query ISearchProductsByKeyword repository.
/// </summary>
internal partial class SearchProductsByKeywordRepository
{

    public async Task<IEnumerable<BookShop.Data.Shared.Entities.Product>> FindProductsByKeywordQueryAsync(string keyword, double similarityThreshold, FilterParameterQuery filterParameterQuery, CancellationToken cancellationToken)
    {

        var query = _products
        .AsQueryable()
        .Where(product =>
                (EF.Functions.FuzzyStringMatchLevenshtein(product.FullName, keyword) <= 5 ||
                EF.Functions.TrigramsSimilarity(product.FullName, keyword) > 0.5 ||
                EF.Functions.FuzzyStringMatchSoundex(product.FullName) == EF.Functions.FuzzyStringMatchSoundex(keyword))
                && product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            );

        query = SearchProductsByKeywordQueryMapper
            .Get()
            .ApplyQuery(query: query, filterParameterQuery: filterParameterQuery);

        return await query
            .AsNoTracking()
            .Skip((filterParameterQuery.PageIndex - 1) * filterParameterQuery.PageSize)
            .Take(filterParameterQuery.PageSize)
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

    public Task<int> GetTotalNumberOfProductsByKeywordQueryAsync(string keyword, CancellationToken cancellationToken)
    {
       return _products.AsNoTracking().Where(product =>
                    EF.Functions.FuzzyStringMatchLevenshtein(product.FullName, keyword) >= 3
                && product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            ).CountAsync(cancellationToken: cancellationToken);
    }
}
