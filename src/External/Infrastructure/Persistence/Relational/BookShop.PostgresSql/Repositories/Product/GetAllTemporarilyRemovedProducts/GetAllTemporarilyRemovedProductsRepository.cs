using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Product.GetAllTemporarilyRemovedProducts;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///    Implement of IGetAllTemporarilyRemovedProducts repository.
/// </summary>
internal partial class GetAllTemporarilyRemovedProductsRepository
    : IGetAllTemporarilyRemovedProductsRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public GetAllTemporarilyRemovedProductsRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
