using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;

/// <summary>
///     Interface for Query GetAllOrderDetailsByUserId Repository
/// </summary>
public partial interface IGetAllOrderDetailsByUserIdRepository
{
    Task<IEnumerable<OrderDetail>> FindOrderDetailsByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
