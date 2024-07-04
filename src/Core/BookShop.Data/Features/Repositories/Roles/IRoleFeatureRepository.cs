using BookShop.Data.Features.Repositories.Roles.CreateRole;

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
}
