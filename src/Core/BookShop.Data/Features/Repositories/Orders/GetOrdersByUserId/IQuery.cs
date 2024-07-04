using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.GetOrdersByUserId;

/// <summary>
///     Interface for Query GetOrdersByUserId Repository
/// </summary>
public partial interface IGetOrdersByUserIdRepository
{
    Task<IEnumerable<Order>> FindOrdersByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
