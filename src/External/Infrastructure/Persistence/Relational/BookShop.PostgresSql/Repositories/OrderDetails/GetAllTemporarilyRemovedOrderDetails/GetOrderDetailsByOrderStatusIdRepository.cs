using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///    Implement of IGetAllTemporarilyRemovedOrderDetails repository.
/// </summary>
internal partial class GetAllTemporarilyRemovedOrderDetailsRepository
    : IGetAllTemporarilyRemovedOrderDetailsRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetail;

    public GetAllTemporarilyRemovedOrderDetailsRepository(BookShopContext context)
    {
        _context = context;
        _orderDetail = _context.Set<OrderDetail>();
    }
}
