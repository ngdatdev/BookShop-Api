using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Orders.RemoveOrderPermanentlyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RemoveOrderPermanentlyById;

/// <summary>
///    Implement of IRemoveOrderPermanentlyByIdRepository repository.
/// </summary>
internal partial class RemoveOrderPermanentlyByIdRepository : IRemoveOrderPermanentlyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;
    private DbSet<OrderDetail> _orderDetails;

    public RemoveOrderPermanentlyByIdRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
        _orderDetails = _context.Set<OrderDetail>();
    }
}
