using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.User.GetProfileUser;

/// <summary>
///     Interface for Query GetProfileUserRepository Repository
/// </summary>
public partial interface IGetProfileUserRepository
{
    Task<bool> IsUserNotTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
