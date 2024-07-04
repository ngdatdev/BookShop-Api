using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.GetOrderById;

/// <summary>
///     Interface for Query GetOrderById Repository
/// </summary>
public partial interface IGetOrderByIdRepository
{
    Task<Order> FindOrderByIdQueryAsync(Guid orderId, CancellationToken cancellationToken);
}
