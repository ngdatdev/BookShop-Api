using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetAllUsersTemporarilyRemovedById;

/// <summary>
///    Implement of query IGetAllUsersTemporarilyRemovedById repository.
/// </summary>
internal partial class GetAllUsersTemporarilyRemovedByIdRepository
{
    public async Task<IEnumerable<UserDetail>> FindAllUsersTemporarilyRemovedByIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _usersDetails
            .AsNoTracking()
            .Where(predicate: userDetail =>
                userDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
                && userDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(selector: userDetail => new UserDetail()
            {
                UserId = userDetail.UserId,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                AvatarUrl = userDetail.AvatarUrl,
                Gender = userDetail.Gender,
                User = new() { UserName = userDetail.User.UserName, Email = userDetail.User.Email, }
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> CountAllTemporarilyRemovedUserQueryAsync(CancellationToken cancellationToken)
    {
        return _usersDetails
            .AsNoTracking()
            .Where(predicate: userDetail =>
                userDetail.RemovedAt != CommonConstant.MIN_DATE_TIME
                && userDetail.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }
}
