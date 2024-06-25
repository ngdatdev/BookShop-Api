using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.User.GetAllUsers;

/// <summary>
///     Interface for Query GetAllUsersRepository Repository
/// </summary>
public partial interface IGetAllUsersRepository
{
    Task<IEnumerable<UserDetail>> FindUserDetailByIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );
}
