using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;
using BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;

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

    /// <summary>
    ///     Gets remove role temporarily by id feature repository.
    /// </summary>
    public IRemoveRoleTemporarilyByIdRepository RemoveRoleTemporarilyByIdRepository { get; }

    /// <summary>
    ///     Gets get all roles temporarily removed by id feature repository.
    /// </summary>
    public IGetAllRolesTemporarilyRemovedRepository GetAllRolesTemporarilyRemovedRepository { get; }
}
