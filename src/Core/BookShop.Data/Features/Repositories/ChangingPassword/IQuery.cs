using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.ChangingPassword;

/// <summary>
///     Interface for Query ChangingPasswordRepository Repository
/// </summary>
public partial interface IChangingPasswordRepository
{
    Task<UserToken> FindUserTokenByResetPasswordTokenQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    );

    Task<bool> IsResetPasswordTokenFoundByItsValueQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserNotTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
