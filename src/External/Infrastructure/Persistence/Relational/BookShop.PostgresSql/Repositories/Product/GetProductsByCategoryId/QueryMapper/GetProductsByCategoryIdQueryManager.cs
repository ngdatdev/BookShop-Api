using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookShop.Data.Shared.FilterAndPagination;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId.QueryMapper;

/// <summary>
///     Manager for GetProductsByCategoryId feature
/// </summary>
/// <summary>
///     Manager for GetProductsByCategoryId feature
/// </summary>
internal class GetProductsByCategoryIdQueryManager
{
    private readonly Dictionary<
        string,
        Func<
            IQueryable<BookShop.Data.Shared.Entities.Product>,
            decimal?,
            decimal?,
            IQueryable<BookShop.Data.Shared.Entities.Product>
        >
    > _filterPriceDictionary;
    private readonly Dictionary<
        string,
        Func<
            IQueryable<BookShop.Data.Shared.Entities.Product>,
            string,
            IQueryable<BookShop.Data.Shared.Entities.Product>
        >
    > _sortDictionary;

    internal GetProductsByCategoryIdQueryManager()
    {
        _filterPriceDictionary = new Dictionary<
            string,
            Func<
                IQueryable<BookShop.Data.Shared.Entities.Product>,
                decimal?,
                decimal?,
                IQueryable<BookShop.Data.Shared.Entities.Product>
            >
        >
        {
            { "MinPrice", (query, minPrice, maxPrice) => query.Where(p => p.Price >= minPrice) },
            { "MaxPrice", (query, minPrice, maxPrice) => query.Where(p => p.Price <= maxPrice) }
        };

        _sortDictionary = new Dictionary<
            string,
            Func<
                IQueryable<BookShop.Data.Shared.Entities.Product>,
                string,
                IQueryable<BookShop.Data.Shared.Entities.Product>
            >
        >
        {
            { "MostBuy", (query, order) => ApplySorting(query, p => p.QuantitySold, order) },
            { "Price", (query, order) => ApplySorting(query, p => p.Price, order) },
            { "Newest", (query, order) => ApplySorting(query, p => p.CreatedAt, order) },
            { "Name", (query, order) => ApplySorting(query, p => p.FullName, order) }
        };
    }

    public IQueryable<BookShop.Data.Shared.Entities.Product> ApplyQuery(
        IQueryable<BookShop.Data.Shared.Entities.Product> query,
        FilterParameterQuery filterParameterQuery
    )
    {
        if (!filterParameterQuery.MaxPrice.HasValue && !filterParameterQuery.MinPrice.HasValue)
        {
            return query;
        }

        foreach (var filter in _filterPriceDictionary)
        {
            query = filter.Value(
                query,
                filterParameterQuery.MinPrice,
                filterParameterQuery.MaxPrice
            );
        }

        if (_sortDictionary.TryGetValue(filterParameterQuery.SortField, out var sortFunc))
        {
            query = sortFunc(query, filterParameterQuery.Order);
        }

        return query;
    }

    private IQueryable<BookShop.Data.Shared.Entities.Product> ApplySorting<TKey>(
        IQueryable<BookShop.Data.Shared.Entities.Product> query,
        Expression<Func<BookShop.Data.Shared.Entities.Product, TKey>> keySelector,
        string order
    )
    {
        return order.ToLower() == "asc"
            ? query.OrderBy(keySelector)
            : query.OrderByDescending(keySelector);
    }
}
