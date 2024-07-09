using BookShop.Data.Features.Repositories.Product.GetProductsByAuthorName;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetProductsByAuthorName;

/// <summary>
///    Implement of IGetProductsByAuthorName repository.
/// </summary>
internal partial class GetProductsByAuthorNameRepository : IGetProductsByAuthorNameRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public GetProductsByAuthorNameRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
