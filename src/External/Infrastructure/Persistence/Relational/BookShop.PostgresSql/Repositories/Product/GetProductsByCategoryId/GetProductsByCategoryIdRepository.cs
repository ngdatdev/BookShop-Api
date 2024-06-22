using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Product.GetProductsByCategoryId;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.FilterAndPagination;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByCategoryId;

/// <summary>
///    Implement of IGetProductsByCategoryIdRepository repository.
/// </summary>
internal partial class GetProductsByCategoryIdRepository : IGetProductsByCategoryIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;
    private DbSet<Category> _categories;

    public GetProductsByCategoryIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
        _categories = _context.Set<Category>();
    }
}
