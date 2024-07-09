using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Carts.ClearCart;

/// <summary>
///    Implement of query IClearCart repository.
/// </summary>
internal partial class ClearCartRepository
{
    public Task<Guid> FindCartIdByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _carts
            .AsNoTracking()
            .Where(predicate: cart => cart.UserId == userId)
            .Select(selector: cart => cart.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
