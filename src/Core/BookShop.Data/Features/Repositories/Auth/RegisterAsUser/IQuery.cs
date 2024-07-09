using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.RegisterAsUser;

/// <summary>
///     Interface for Query RefreshToken Repository
/// </summary>
public partial interface IRegisterAsUserRepository
{
    Task<bool> IsUserFoundByNormalizedEmailQueryAsync(string email, CancellationToken cancellation);

    Task<bool> IsUserFoundByNormalizedUsernameQueryAsync(
        string username,
        CancellationToken cancellation
    );
}
