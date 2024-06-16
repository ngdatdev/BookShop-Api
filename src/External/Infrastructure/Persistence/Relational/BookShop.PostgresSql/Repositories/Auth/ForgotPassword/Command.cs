using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.PostgresSql.Repositories.Auth.ForgotPassword;

/// <summary>
///    Implement of command forgot password repository.
/// </summary>
internal partial class ForgotPasswordRepository
{
    public async Task<bool> AddResetPasswordTokenCommandAsync(
        UserToken newResetPasswordToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _userTokens.AddAsync(
                entity: newResetPasswordToken,
                cancellationToken: cancellationToken
            );
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}
