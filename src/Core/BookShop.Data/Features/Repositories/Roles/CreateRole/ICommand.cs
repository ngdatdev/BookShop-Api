using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Features.Repositories.Roles.CreateRole;

/// <summary>
///     Interface for Command CreateRoleRepository
/// </summary>
public partial interface ICreateRoleRepository
{
    Task<bool> CreateRoleCommandAsync(
        Role newRole,
        RoleManager<Role> roleManager,
        CancellationToken cancellationToken
    );
}
