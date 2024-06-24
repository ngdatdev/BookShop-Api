using BookShop.Data.Features.Repositories.Product.RemoveProductPermanentlyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductPermanentlyById;

/// <summary>
///    Implement of IRemoveProductPermanentlyByIdRepository repository.
/// </summary>
internal partial class RemoveProductPermanentlyByIdRepository
    : IRemoveProductPermanentlyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;
    private DbSet<Asset> _assets;
    private DbSet<ProductCategory> _productCategories;

    public RemoveProductPermanentlyByIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
        _assets = _context.Set<Asset>();
        _productCategories = _context.Set<ProductCategory>();
    }
}
