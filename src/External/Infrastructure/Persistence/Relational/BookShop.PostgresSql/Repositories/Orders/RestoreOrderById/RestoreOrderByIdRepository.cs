using BookShop.Data.Features.Repositories.Orders.RestoreOrderById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RestoreOrderById;

/// <summary>
///    Implement of IRestoreOrderByIdRepository repository.
/// </summary>
internal partial class RestoreOrderByIdRepository : IRestoreOrderByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Order> _orders;

    public RestoreOrderByIdRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<BookShop.Data.Shared.Entities.Order>();
    }
}
