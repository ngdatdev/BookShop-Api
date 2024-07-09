using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.Login;

/// <summary>
///     Interface for Command Login Repository
/// </summary>
public partial interface ILoginRepository
{
    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
