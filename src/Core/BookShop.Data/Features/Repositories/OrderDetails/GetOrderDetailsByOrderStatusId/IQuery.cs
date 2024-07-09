using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///     Interface for Query GetOrderDetailsByOrderStatusId Repository
/// </summary>
public partial interface IGetOrderDetailsByOrderStatusIdRepository
{
    Task<bool> IsOrderStatusFoundById(Guid orderStatusId, CancellationToken cancellationToken);

    Task<IEnumerable<OrderDetail>> FindOrderDetailsByStatusIdAndUserIdQueryAsync(
        Guid orderStatusId,
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<int> CountNumberOfOrderDetailsByOrderStatusIdQueryAsync(
        Guid orderStatusId,
        CancellationToken cancellationToken
    );
}
