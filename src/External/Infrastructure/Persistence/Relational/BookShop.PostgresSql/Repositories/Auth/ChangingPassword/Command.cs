using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ChangingPassword;

/// <summary>
///    Implement of command ChangingPassword repository.
/// </summary>
internal partial class ChangingPasswordRepository
{
    public async Task<bool> RemoveUserResetPasswordTokenCommandAsync(
        string resetPasswordToken,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _userTokens
                        .Where(predicate: userToken => userToken.Value.Equals(resetPasswordToken))
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
