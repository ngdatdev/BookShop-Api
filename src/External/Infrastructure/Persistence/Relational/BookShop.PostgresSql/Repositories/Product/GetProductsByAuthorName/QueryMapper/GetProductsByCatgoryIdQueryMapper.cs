using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByAuthorName.QueryMapper;

internal static class GetProductsByCatgoryIdQueryMapper
{
    private static GetProductsByAuthorNameQueryManager _getProductsByCategoryIdQueryManager;

    internal static GetProductsByAuthorNameQueryManager Get()
    {
        return _getProductsByCategoryIdQueryManager ??= new();
    }
}
