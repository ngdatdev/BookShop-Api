using BookShop.Data.Features.Repositories.Product.SearchProductsByKeyword;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.PostgresSql.Repositories.Product.SearchProductsByKeyword;

/// <summary>
///    Implement of ISearchProductsByKeyword repository.
/// </summary>
internal partial class SearchProductsByKeywordRepository : ISearchProductsByKeywordRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public SearchProductsByKeywordRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
