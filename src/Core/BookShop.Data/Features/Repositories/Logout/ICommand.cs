using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Logout;

/// <summary>
///     Interface for Command LogoutRepository Repository
/// </summary>
public partial interface ILogoutRepository
{
    Task<bool> RemoveRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
