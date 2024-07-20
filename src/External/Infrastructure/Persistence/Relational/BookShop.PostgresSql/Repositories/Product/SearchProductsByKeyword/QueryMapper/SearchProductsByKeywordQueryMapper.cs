using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.PostgresSql.Repositories.Product.SearchProductsByKeyword.QueryMapper;

internal static class SearchProductsByKeywordQueryMapper
{
    private static SearchProductsByKeywordQueryManager _getProductsByCategoryIdQueryManager;

    internal static SearchProductsByKeywordQueryManager Get()
    {
        return _getProductsByCategoryIdQueryManager ??= new();
    }
}
