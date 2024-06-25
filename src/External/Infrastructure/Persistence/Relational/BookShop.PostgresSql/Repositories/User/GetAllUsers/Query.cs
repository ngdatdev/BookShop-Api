using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetAllUsers;

/// <summary>
///    Implement of query IGetAllUsersRepository repository.
/// </summary>
internal partial class GetAllUsersRepository
{
    public async Task<
        IEnumerable<BookShop.Data.Shared.Entities.UserDetail>
    > FindUserDetailByIdQueryAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _usersDetails
            .AsNoTracking()
            .Where(predicate: userDetail =>
                userDetail.RemovedAt == CommonConstant.MIN_DATE_TIME
                && userDetail.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(selector: userDetail => new BookShop.Data.Shared.Entities.UserDetail()
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
}
