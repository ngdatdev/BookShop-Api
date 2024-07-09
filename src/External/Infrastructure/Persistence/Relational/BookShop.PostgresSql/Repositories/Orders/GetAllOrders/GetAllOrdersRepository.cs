using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Orders.GetAllOrders;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetAllOrders;

/// <summary>
///    Implement of IGetAllOrders repository.
/// </summary>
internal partial class GetAllOrdersRepository : IGetAllOrdersRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;

    public GetAllOrdersRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
    }
}
