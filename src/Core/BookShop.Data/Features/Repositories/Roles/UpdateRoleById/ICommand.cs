using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.UpdateRoleById;

/// <summary>
///     Interface for Command UpdateRoleByIdRepository
/// </summary>
public partial interface IUpdateRoleByIdRepository
{
    Task<bool> UpdateRoleByIdCommandAsync(
        Guid roleId,
        string roleName,
        DateTime updatedAt,
        Guid updatedBy,
        CancellationToken cancellationToken
    );
}
