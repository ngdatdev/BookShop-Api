using BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToCancel;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///    Implement of ISwitchOrderStatusToCancel repository.
/// </summary>
internal partial class SwitchOrderStatusToCancelRepository : ISwitchOrderStatusToCancelRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetails;
    private DbSet<OrderStatus> _orderStatus;

    public SwitchOrderStatusToCancelRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<OrderDetail>();
        _orderStatus = _context.Set<OrderStatus>();
    }
}
