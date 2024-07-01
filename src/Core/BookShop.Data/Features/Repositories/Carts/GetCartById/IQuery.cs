using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Carts.GetCartById;

/// <summary>
///     Interface for Query GetCartById Repository
/// </summary>
public partial interface IGetCartByIdRepository
{
    Task<Cart> FindCartByIdQueryAsync(Guid userId, CancellationToken cancellationToken);
}
