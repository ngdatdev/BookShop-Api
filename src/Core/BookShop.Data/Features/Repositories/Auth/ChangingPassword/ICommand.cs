using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.ChangingPassword;

/// <summary>
///     Interface for Command ChangingPassword Repository
/// </summary>
public partial interface IChangingPasswordRepository
{
    Task<bool> RemoveUserResetPasswordTokenCommandAsync(
        string resetPasswordToken,
        CancellationToken cancellationToken
    );
}
