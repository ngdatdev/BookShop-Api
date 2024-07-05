using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///     Interface for Query GetAllTemporarilyRemovedOrderDetails Repository
/// </summary>
public partial interface IGetAllTemporarilyRemovedOrderDetailsRepository
{
    Task<IEnumerable<OrderDetail>> FindAllTemporarilyRemovedOrderDetailsQueryAsync(
        CancellationToken cancellationToken
    );
}
