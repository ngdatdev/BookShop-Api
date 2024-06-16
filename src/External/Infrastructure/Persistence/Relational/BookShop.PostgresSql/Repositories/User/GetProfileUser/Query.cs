using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetProfileUser;

/// <summary>
///    Implement of query IGetProfileUserRepository repository.
/// </summary>
internal partial class GetProfileUserRepository
{
    public Task<UserDetail> GetUserDetailByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _usersDetails
            .AsNoTracking()
            .Where(predicate: userDetail => userDetail.UserId == userId)
            .Select(selector: userDetail => new UserDetail()
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                AvatarUrl = userDetail.AvatarUrl,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserNotTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _usersDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt == CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
