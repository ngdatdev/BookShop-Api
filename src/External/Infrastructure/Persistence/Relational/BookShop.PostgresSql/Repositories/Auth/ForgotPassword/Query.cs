using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ForgotPassword;

/// <summary>
///    Implement of query forgot password repository.
/// </summary>
internal partial class ForgotPasswordRepository
{
    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
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
