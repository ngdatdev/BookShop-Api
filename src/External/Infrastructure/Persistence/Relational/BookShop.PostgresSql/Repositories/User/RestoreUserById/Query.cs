using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RestoreUserById;

/// <summary>
///    Implement of query IRestoreUserByIdRepository repository.
/// </summary>
internal partial class RestoreUserByIdRepository
{
    public Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _users.AnyAsync(
            predicate: user => user.Id == userId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: user =>
                    user.UserId == userId
                    && user.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && user.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
