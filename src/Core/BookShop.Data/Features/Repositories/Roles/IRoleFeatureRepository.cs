using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;

namespace BookShop.Data.Features.Repositories.Roles;

/// <summary>
///     Interface for role repository manager.
/// </summary>
public interface IRoleFeatureRepository
{
    /// <summary>
    ///     Gets create role feature repository.
    /// </summary>
    public ICreateRoleRepository CreateRoleRepository { get; }

    /// <summary>
    ///     Gets get all roles feature repository.
    /// </summary>
    public IGetAllRolesRepository GetAllRolesRepository { get; }
}
