using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserTemporarilyById;

/// <summary>
///    Implement of query IRemoveUserTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveUserTemporarilyByIdRepository
{
    public Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _users.AnyAsync(
            predicate: product => product.Id == userId,
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
                predicate: userDetail =>
                    userDetail.UserId == userId
                    && userDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && userDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
