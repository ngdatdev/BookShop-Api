using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserPermanentlyById;

/// <summary>
///    Implement of query IRemoveUserPermanentlyByIdRepository repository.
/// </summary>
internal partial class RemoveUserPermanentlyByIdRepository
{
    public Task<string> FindAvatarUserByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user => user.Id == userId)
            .Select(selector: user => user.UserDetail.AvatarUrl)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
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
