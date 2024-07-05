using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using static BookShop.Application.Features.Users.GetAllUsers.GetAllUsersResponse.Body;

namespace BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;

/// <summary>
///    Implement of query IGetOrderDetailById repository.
/// </summary>
internal partial class GetOrderDetailByIdRepository
{
    public Task<OrderDetail> FindOrderDetailByIdQueryAsync(
        Guid orderDetailId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetail
            .AsNoTracking()
            .Where(predicate: orderDetail =>
                orderDetail.Id == orderDetailId && orderDetail.Order.UserDetail.UserId == userId
            )
            .Select(selector: orderDetail => new OrderDetail()
            {
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
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsOrderDetailFoundByUserIdAndOrderDetailIdQueryAsync(
        Guid userId,
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetail
            .AsNoTracking()
            .AnyAsync(
                predicate: orderDetail =>
                    orderDetail.Id == orderDetailId
                    && orderDetail.Order.UserDetail.UserId == userId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsOrderDetailTemporarilyRemovedById(
        Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        return _orderDetail
            .AsNoTracking()
            .AnyAsync(
                predicate: orderDetail =>
                    orderDetail.Id == orderDetailId
                    && orderDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                    && orderDetail.RemovedAt != CommonConstant.MIN_DATE_TIME,
                cancellationToken: cancellationToken
            );
    }
}
