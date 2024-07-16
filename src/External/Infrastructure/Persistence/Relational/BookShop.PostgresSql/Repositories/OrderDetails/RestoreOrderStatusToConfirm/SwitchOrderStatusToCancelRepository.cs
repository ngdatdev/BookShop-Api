using BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderStatusToConfirm;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///    Implement of IRestoreOrderStatusToConfirm repository.
/// </summary>
internal partial class RestoreOrderStatusToConfirmRepository
    : IRestoreOrderStatusToConfirmRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetails;
    private DbSet<OrderStatus> _orderStatus;

    public RestoreOrderStatusToConfirmRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<OrderDetail>();
        _orderStatus = _context.Set<OrderStatus>();
    }
}
