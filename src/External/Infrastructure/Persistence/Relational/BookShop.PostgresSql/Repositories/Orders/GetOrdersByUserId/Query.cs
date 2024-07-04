using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetOrdersByUserId;

/// <summary>
///    Implement of query IGetOrdersByUserId repository.
/// </summary>
internal partial class GetOrdersByUserIdRepository
{
    public async Task<IEnumerable<Order>> FindOrdersByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _orders
            .AsNoTracking()
            .Where(predicate: order =>
                order.UserId == userId
                && order.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && order.RemovedAt == CommonConstant.MIN_DATE_TIME
            )
            .Select(order => new Order()
            {
                OrderDate = order.OrderDate,
                TotalCost = order.TotalCost,
                UserDetail = new()
                {
                    FirstName = order.UserDetail.FirstName,
                    LastName = order.UserDetail.LastName,
                },
                Address = new()
                {
                    Ward = order.Address.Ward,
                    District = order.Address.District,
                    Province = order.Address.Province,
                },
                OrderDetails = order.OrderDetails.Select(orderDetail => new OrderDetail()
                {
                    Id = orderDetail.Id,
                    Quantity = orderDetail.Quantity,
                    Cost = orderDetail.Cost,
                    OrderStatus = new() { FullName = orderDetail.OrderStatus.FullName, },
                    Product = new()
                    {
                        Id = orderDetail.Product.Id,
                        FullName = orderDetail.Product.FullName,
                        Author = orderDetail.Product.Author,
                        ImageUrl = orderDetail.Product.ImageUrl,
                        Price = orderDetail.Product.Price,
                    }
                })
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
