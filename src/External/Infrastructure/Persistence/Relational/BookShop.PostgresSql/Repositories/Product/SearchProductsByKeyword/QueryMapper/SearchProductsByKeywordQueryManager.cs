using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookShop.Data.Shared.FilterAndPagination;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.PostgresSql.Repositories.Product.SearchProductsByKeyword.QueryMapper;

/// <summary>
///     Manager for SearchProductsByKeyword feature
/// </summary>
internal class SearchProductsByKeywordQueryManager
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

    private readonly Dictionary<
        string,
        Func<
            IQueryable<BookShop.Data.Shared.Entities.Product>,
            string,
            IQueryable<BookShop.Data.Shared.Entities.Product>
        >
    > _filterFieldDictionary;


    internal SearchProductsByKeywordQueryManager()
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

        _filterFieldDictionary = new Dictionary<
            string,
            Func<
                IQueryable<BookShop.Data.Shared.Entities.Product>,
                string,
                IQueryable<BookShop.Data.Shared.Entities.Product>
            >
        >
        {
            { "CategoryName", (query, category) => query.Where(p => p.ProductCategories.Any(pc => pc.Category.FullName.Contains(category) ))}
            // More field...
        };

    }

    public IQueryable<BookShop.Data.Shared.Entities.Product> ApplyQuery(
        IQueryable<BookShop.Data.Shared.Entities.Product> query,
        FilterParameterQuery filterParameterQuery
    )
    {
        if (filterParameterQuery.MaxPrice.HasValue && filterParameterQuery.MinPrice.HasValue)
        {
            foreach (var filter in _filterPriceDictionary)
            {
                query = filter.Value(
                    query,
                    filterParameterQuery.MinPrice,
                    filterParameterQuery.MaxPrice
                );
            }
        }

        if(filterParameterQuery.Filters != null && filterParameterQuery.Filters.Any())
        {
            foreach (var filterField in filterParameterQuery.Filters)
            {
                if(_filterFieldDictionary.TryGetValue(filterField.Key, out var filterFunc)) { 
                    query = filterFunc(query, filterField.Value);
                }
            }
        }

        if (filterParameterQuery.SortField != null && _sortDictionary.TryGetValue(filterParameterQuery.SortField, out var sortFunc))
        {
            query = sortFunc(query, filterParameterQuery.Order);
        }
        else
        {
            query = query.OrderBy(product => product.CreatedAt);
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
