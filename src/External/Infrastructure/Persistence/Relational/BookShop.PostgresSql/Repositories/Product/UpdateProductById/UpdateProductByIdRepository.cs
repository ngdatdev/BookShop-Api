using BookShop.Data.Features.Repositories.Product.UpdateProductById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.UpdateProductById;

/// <summary>
///    Implement of IUpdateProductById repository.
/// </summary>
internal partial class UpdateProductByIdRepository : IUpdateProductByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;
    private DbSet<Category> _categories;
    private DbSet<Asset> _assets;
    private DbSet<ProductCategory> _productCategories;

    public UpdateProductByIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
        _categories = _context.Set<Category>();
        _assets = _context.Set<Asset>();
        _productCategories = _context.Set<ProductCategory>();
    }
}
