using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Orders.CreateOrder;

/// <summary>
///     Interface for Query CreateOrder Repository
/// </summary>
public partial interface ICreateOrderRepository
{
    Task<bool> IsProductsFoundByIdQueryAsync(
        IEnumerable<Guid> productIds,
        CancellationToken cancellationToken
    );

    Task<bool> IsProductsTemporarilyRemovedQueryAsync(
        IEnumerable<Guid> productIds,
        CancellationToken cancellationToken
    );

    Task<Guid> FindAddressIdFoundByNameQueryAsync(
        string ward,
        string district,
        string province,
        CancellationToken cancellationToken
    );
}
