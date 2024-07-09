using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
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

    public Task<int> CountNumberOfOrderDetailsByOrderStatusIdQueryAsync(
        Guid orderStatusId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetail
            .Where(predicate: orderDetail =>
                orderDetail.OrderStatusId == orderStatusId
                && orderDetail.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && orderDetail.RemovedAt == CommonConstant.MIN_DATE_TIME
            )
            .CountAsync(cancellationToken: cancellationToken);
    }
}
