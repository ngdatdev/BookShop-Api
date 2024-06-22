using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId.QueryMapper;

internal static class GetProductsByCatgoryIdQueryMapper
{
    private static GetProductsByCategoryIdQueryManager _getProductsByCategoryIdQueryManager;

    internal static GetProductsByCategoryIdQueryManager Get()
    {
        return _getProductsByCategoryIdQueryManager ??= new();
    }
}
