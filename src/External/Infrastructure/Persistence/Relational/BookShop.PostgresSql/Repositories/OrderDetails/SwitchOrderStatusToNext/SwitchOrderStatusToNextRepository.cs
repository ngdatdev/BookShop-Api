using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToNext;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///    Implement of ISwitchOrderStatusToNext repository.
/// </summary>
internal partial class SwitchOrderStatusToNextRepository : ISwitchOrderStatusToNextRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetails;
    private DbSet<OrderStatus> _orderStatus;

    public SwitchOrderStatusToNextRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<OrderDetail>();
        _orderStatus = _context.Set<OrderStatus>();
    }
}
