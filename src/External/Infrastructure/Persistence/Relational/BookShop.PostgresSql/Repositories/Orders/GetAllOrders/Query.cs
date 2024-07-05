using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.GetAllOrders;

/// <summary>
///    Implement of query IGetAllOrders repository.
/// </summary>
internal partial class GetAllOrdersRepository
{
    public async Task<IEnumerable<Order>> FindAllOrdersQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _orders
            .AsNoTracking()
            .Where(predicate: order =>
                order.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && order.RemovedAt == CommonConstant.MIN_DATE_TIME
            )
            .OrderBy(order => order.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
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
