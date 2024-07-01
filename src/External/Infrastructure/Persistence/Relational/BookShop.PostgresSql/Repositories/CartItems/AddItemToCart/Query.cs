using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of query IAddItemToCartRepository repository.
/// </summary>
internal partial class AddItemToCartRepository
{
    public Task<string> FindCartIdByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsProductFoundByIdQueryAsync(
        Guid productId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
