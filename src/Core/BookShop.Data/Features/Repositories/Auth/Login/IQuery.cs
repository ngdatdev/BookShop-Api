using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.Login;

/// <summary>
///     Interface for Query LoginRepository Repository
/// </summary>
public partial interface ILoginRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
