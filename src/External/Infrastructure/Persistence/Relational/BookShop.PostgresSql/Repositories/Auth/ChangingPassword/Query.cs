using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ChangingPassword;

/// <summary>
///    Implement of query changing password repository.
/// </summary>
internal partial class ChangingPasswordRepository
{
    public Task<UserToken> FindUserTokenByResetPasswordTokenQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    )
    {
        return _userTokens
            .AsNoTracking()
            .Where(predicate: userToken => userToken.Value == passwordResetToken)
            .Select(selector: userToken => new UserToken { UserId = userToken.UserId, })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsResetPasswordTokenFoundByItsValueQueryAsync(
        string passwordResetToken,
        CancellationToken cancellationToken
    )
    {
        return _userTokens.AnyAsync(
            predicate: userToken => userToken.Value.Equals(passwordResetToken),
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserNotTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _usersDetail.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt == CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
