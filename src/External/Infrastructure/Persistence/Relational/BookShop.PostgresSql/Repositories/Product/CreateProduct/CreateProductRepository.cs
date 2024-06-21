using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Product.CreateProduct;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.CreateProduct;

/// <summary>
///    Implement of ICreateProductRepository repository.
/// </summary>
internal partial class CreateProductRepository : ICreateProductRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;
    private DbSet<Category> _categories;

    public CreateProductRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
        _categories = _context.Set<Category>();
    }
}
