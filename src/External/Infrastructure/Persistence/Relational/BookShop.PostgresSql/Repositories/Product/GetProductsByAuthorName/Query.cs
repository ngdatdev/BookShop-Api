using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.FilterAndPagination;
using BookShop.PostgresSql.Repositories.Product.GetProductsByAuthorName.QueryMapper;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByAuthorName;

/// <summary>
///    Implement of query IGetProductsByAuthorNameRepository repository.
/// </summary>
internal partial class GetProductsByAuthorNameRepository
{
    public async Task<
        IEnumerable<BookShop.Data.Shared.Entities.Product>
    > FindProductsByAuthorNameQueryAsync(
        string authorName,
        FilterParameterQuery filterParameterQuery,
        CancellationToken cancellationToken
    )
    {
        var query = _products
            .AsQueryable()
            .Where(product =>
                EF.Functions.Collate(
                        product.Author,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(authorName)
                && product.RemovedAt == CommonConstant.MIN_DATE_TIME
                && product.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            );

        query = GetProductsByCatgoryIdQueryMapper
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

    public Task<int> GetTotalNumberOfProducts(
        string authorName,
        CancellationToken cancellationToken
    )
    {
        return _products
            .Where(predicate: entity => entity.Author.Equals(authorName))
            .CountAsync(cancellationToken: cancellationToken);
    }
}
