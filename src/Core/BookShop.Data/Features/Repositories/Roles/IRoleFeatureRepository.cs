using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;
using BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Data.Features.Repositories.Roles.RemoveRolePermanentlyById;
using BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;
using BookShop.Data.Features.Repositories.Roles.RestoreRoleById;
using BookShop.Data.Features.Repositories.Roles.UpdateRoleById;

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

    /// <summary>
    ///     Gets restore role by id feature repository.
    /// </summary>
    public IRestoreRoleByIdRepository RestoreRoleByIdRepository { get; }

    /// <summary>
    ///     Gets remove role permanently by id feature repository.
    /// </summary>
    public IRemoveRolePermanentlyByIdRepository RemoveRolePermanentlyByIdRepository { get; }

    /// <summary>
    ///     Gets update role by id feature repository.
    /// </summary>
    public IUpdateRoleByIdRepository UpdateRoleByIdRepository { get; }
}
