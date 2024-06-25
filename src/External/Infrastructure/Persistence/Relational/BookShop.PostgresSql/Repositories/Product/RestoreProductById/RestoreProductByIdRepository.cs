using BookShop.Data.Features.Repositories.Product.RestoreProductById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RestoreProductById;

/// <summary>
///    Implement of IRestoreProductByIdRepository repository.
/// </summary>
internal partial class RestoreProductByIdRepository : IRestoreProductByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public RestoreProductByIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
