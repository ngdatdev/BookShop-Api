using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///    Implement of query IGetOrderDetailsByOrderStatusId repository.
/// </summary>
internal partial class GetOrderDetailsByOrderStatusIdRepository
{
    public Task<bool> IsOrderStatusFoundById(
        Guid orderStatusId,
        CancellationToken cancellationToken
    )
    {
        return _orderStatus
            .AsNoTracking()
            .AnyAsync(
                predicate: orderStatus => orderStatus.Id == orderStatusId,
                cancellationToken: cancellationToken
            );
    }

    public async Task<IEnumerable<OrderDetail>> FindOrderDetailsByStatusIdAndUserIdQueryAsync(
        Guid orderStatusId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _orderDetail
            .AsNoTracking()
            .Where(predicate: orderDetail =>
                orderDetail.OrderStatusId == orderStatusId
                && orderDetail.Order.UserDetail.UserId == userId
            )
            .Select(selector: orderDetail => new OrderDetail()
            {
                Id = orderDetail.Id,
                Cost = orderDetail.Cost,
                OrderStatus = orderDetail.OrderStatus,
                Product = new()
                {
                    Author = orderDetail.Product.Author,
                    Discount = orderDetail.Product.Discount,
                    FullName = orderDetail.Product.FullName,
                    ImageUrl = orderDetail.Product.ImageUrl,
                    Price = orderDetail.Product.Price,
                },
                Quantity = orderDetail.Quantity,
                Order = new Order()
                {
                    Address = new()
                    {
                        Ward = orderDetail.Order.Address.Ward,
                        District = orderDetail.Order.Address.District,
                        Province = orderDetail.Order.Address.Province,
                    },
                    UserDetail = new()
                    {
                        FirstName = orderDetail.Order.UserDetail.FirstName,
                        LastName = orderDetail.Order.UserDetail.LastName,
                    },
                    OrderDate = orderDetail.Order.OrderDate,
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
