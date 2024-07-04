using BookShop.Data.Features.Repositories.Orders.GetOrderById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetOrderById;

/// <summary>
///    Implement of IGetOrderById repository.
/// </summary>
internal partial class GetOrderByIdRepository : IGetOrderByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;

    public GetOrderByIdRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
    }
}
