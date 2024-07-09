using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.User.GetAllUsersTemporarilyRemovedById;

/// <summary>
///     Interface for Query GetAllUsersTemporarilyRemovedById Repository
/// </summary>
public partial interface IGetAllUsersTemporarilyRemovedByIdRepository
{
    Task<IEnumerable<UserDetail>> FindAllUsersTemporarilyRemovedByIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountAllTemporarilyRemovedUserQueryAsync(CancellationToken cancellationToken);
}
