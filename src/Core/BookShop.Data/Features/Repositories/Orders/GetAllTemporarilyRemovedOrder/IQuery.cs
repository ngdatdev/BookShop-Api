using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.GetAllTemporarilyRemovedOrder;

/// <summary>
///     Interface for Query GetAllTemporarilyRemovedOrder Repository
/// </summary>
public partial interface IGetAllTemporarilyRemovedOrderRepository
{
    Task<IEnumerable<Order>> FindAllOrdersTemporarilyRemovedQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalNumberOfOrders(CancellationToken cancellationToken);
}
