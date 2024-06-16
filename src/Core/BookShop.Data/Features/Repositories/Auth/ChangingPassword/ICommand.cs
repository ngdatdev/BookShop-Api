using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.ChangingPassword;

/// <summary>
///     Interface for Command ChangingPasswordRepository Repository
/// </summary>
public partial interface IChangingPasswordRepository
{
    Task<bool> RemoveUserResetPasswordTokenCommandAsync(
        string resetPasswordToken,
        CancellationToken cancellationToken
    );
}
