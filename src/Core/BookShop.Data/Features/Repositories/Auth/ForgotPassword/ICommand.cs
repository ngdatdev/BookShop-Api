using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.ForgotPassword;

/// <summary>
///     Interface for Command ForgotPassword Repository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> AddResetPasswordTokenCommandAsync(
        UserToken newResetPasswordToken,
        CancellationToken cancellationToken
    );
}
