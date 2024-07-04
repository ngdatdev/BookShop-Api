using BookShop.Data.Features.Repositories.Orders.GetOrdersByUserId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetOrdersByUserId;

/// <summary>
///    Implement of IGetOrdersByUserId repository.
/// </summary>
internal partial class GetOrdersByUserIdRepository : IGetOrdersByUserIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;

    public GetOrdersByUserIdRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
    }
}
