using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;

/// <summary>
///    Implement of IGetOrderDetailById repository.
/// </summary>
internal partial class GetOrderDetailByIdRepository : IGetOrderDetailByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetail;

    public GetOrderDetailByIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetail = _context.Set<OrderDetail>();
    }
}
