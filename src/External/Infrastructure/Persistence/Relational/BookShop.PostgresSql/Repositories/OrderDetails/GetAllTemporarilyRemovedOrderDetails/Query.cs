using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///    Implement of query IGetAllTemporarilyRemovedOrderDetails repository.
/// </summary>
internal partial class GetAllTemporarilyRemovedOrderDetailsRepository
{
    public async Task<IEnumerable<OrderDetail>> FindAllTemporarilyRemovedOrderDetailsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _orderDetail
            .AsNoTracking()
            .Where(predicate: orderDetail =>
                orderDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && orderDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
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

    public Task<int> CountNumberOfTemporarilyRemovedOrderDetailsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return _orderDetail
            .AsNoTracking()
            .Where(predicate: orderDetail =>
                orderDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && orderDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
            )
            .CountAsync(cancellationToken: cancellationToken);
    }
}
