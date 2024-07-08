using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Product.GetAllProducts;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetAllProducts;

/// <summary>
///    Implement of IGetAllProductsRepository repository.
/// </summary>
internal partial class GetAllProductsRepository : IGetAllProductsRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public GetAllProductsRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }

  
}
