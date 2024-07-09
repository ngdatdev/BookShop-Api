using BookShop.Data.Features.Repositories.Product.GetProductById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductById;

/// <summary>
///    Implement of IGetProductById repository.
/// </summary>
internal partial class GetProductByIdRepository : IGetProductByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public GetProductByIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
