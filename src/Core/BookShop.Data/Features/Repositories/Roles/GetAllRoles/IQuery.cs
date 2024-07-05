using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Roles.GetAllRoles;

/// <summary>
///     Interface for Query GetAllRolesRepository Repository
/// </summary>
public partial interface IGetAllRolesRepository
{
    Task<IEnumerable<Role>> FindAllRolesQueryAsync(CancellationToken cancellationToken);
}
