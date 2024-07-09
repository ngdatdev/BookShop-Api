using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Orders.GetAllOrders;

/// <summary>
///     Interface for Query GetAllOrders Repository
/// </summary>
public partial interface IGetAllOrdersRepository
{
    Task<IEnumerable<Order>> FindAllOrdersQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountTotalNumberOfOrders(CancellationToken cancellationToken);
}
