using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///    Implement of IGetOrderDetailsByOrderStatusId repository.
/// </summary>
internal partial class GetOrderDetailsByOrderStatusIdRepository
    : IGetOrderDetailsByOrderStatusIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetail;
    private DbSet<OrderStatus> _orderStatus;

    public GetOrderDetailsByOrderStatusIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetail = _context.Set<OrderDetail>();
        _orderStatus = _context.Set<OrderStatus>();
    }
}
