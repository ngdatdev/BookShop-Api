using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetAllOrderDetailsByUserId;

/// <summary>
///    Implement of IGetAllOrderDetailsByUserId repository.
/// </summary>
internal partial class GetAllOrderDetailsByUserIdRepository : IGetAllOrderDetailsByUserIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetail;

    public GetAllOrderDetailsByUserIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetail = _context.Set<OrderDetail>();
    }
}
