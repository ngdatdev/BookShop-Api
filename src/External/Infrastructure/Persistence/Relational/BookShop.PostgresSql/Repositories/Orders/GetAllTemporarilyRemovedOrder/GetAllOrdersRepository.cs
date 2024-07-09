using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Orders.GetAllTemporarilyRemovedOrder;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetAllTemporarilyRemovedOrder;

/// <summary>
///    Implement of IGetAllTemporarilyRemovedOrder repository.
/// </summary>
internal partial class GetAllTemporarilyRemovedOrderRepository
    : IGetAllTemporarilyRemovedOrderRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;

    public GetAllTemporarilyRemovedOrderRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
    }
}
