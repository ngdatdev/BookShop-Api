using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.CreateRole;

/// <summary>
///     Interface for Query CreateRoleRepository Repository
/// </summary>
public partial interface ICreateRoleRepository
{
    Task<bool> IsRoleTemporarilyRemovedByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken
    );

    Task<bool> IsSameRoleNameFoundByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken
    );
}
