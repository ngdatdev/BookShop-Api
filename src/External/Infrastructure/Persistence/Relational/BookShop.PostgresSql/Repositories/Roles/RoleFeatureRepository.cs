using BookShop.Data.Features.Repositories.Orders.RemoveOrderPermanentlyById;
using BookShop.Data.Features.Repositories.Roles;
using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;
using BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Data.Features.Repositories.Roles.RemoveRolePermanentlyById;
using BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;
using BookShop.Data.Features.Repositories.Roles.RestoreRoleById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Roles.CreateRole;
using BookShop.PostgresSql.Repositories.Roles.GetAllRoles;
using BookShop.PostgresSql.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.PostgresSql.Repositories.Roles.RemoveRolePermanentlyById;
using BookShop.PostgresSql.Repositories.Roles.RemoveRoleTemporarilyById;
using BookShop.PostgresSql.Repositories.Roles.RestoreRoleById;

namespace BookShop.PostgresSql.Repositories.Roles;

/// <summary>
///    Implement of RoleFeatureRepository interface.
/// </summary>
internal class RoleFeatureRepository : IRoleFeatureRepository
{
    private readonly BookShopContext _context;
    private ICreateRoleRepository _createRoleRepository;
    private IGetAllRolesRepository _getAllRolesRepository;
    private IRemoveRoleTemporarilyByIdRepository _removeRoleTemporarilyByIdRepository;
    private IGetAllRolesTemporarilyRemovedRepository _getAllRolesTemporarilyRemovedRepository;
    private IRestoreRoleByIdRepository _restoreRoleByIdRepository;
    private IRemoveRolePermanentlyByIdRepository _removeRolePermanentlyByIdRepository;

    internal RoleFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public ICreateRoleRepository CreateRoleRepository
    {
        get { return _createRoleRepository ??= new CreateRoleRepository(context: _context); }
    }

    public IGetAllRolesRepository GetAllRolesRepository
    {
        get { return _getAllRolesRepository ??= new GetAllRolesRepository(context: _context); }
    }

    public IRemoveRoleTemporarilyByIdRepository RemoveRoleTemporarilyByIdRepository
    {
        get
        {
            return _removeRoleTemporarilyByIdRepository ??= new RemoveRoleTemporarilyByIdRepository(
                context: _context
            );
        }
    }

    public IGetAllRolesTemporarilyRemovedRepository GetAllRolesTemporarilyRemovedRepository
    {
        get
        {
            return _getAllRolesTemporarilyRemovedRepository ??=
                new GetAllRolesTemporarilyRemovedRepository(context: _context);
        }
    }

    public IRestoreRoleByIdRepository RestoreRoleByIdRepository
    {
        get
        {
            return _restoreRoleByIdRepository ??= new RestoreRoleByIdRepository(context: _context);
        }
    }

    public IRemoveRolePermanentlyByIdRepository RemoveRolePermanentlyByIdRepository
    {
        get
        {
            return _removeRolePermanentlyByIdRepository ??= new RemoveRolePermanentlyByIdRepository(
                context: _context
            );
        }
    }
}
