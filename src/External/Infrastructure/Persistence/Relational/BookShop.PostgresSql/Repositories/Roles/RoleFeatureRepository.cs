using BookShop.Data.Features.Repositories.Roles;
using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;
using BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Roles.CreateRole;
using BookShop.PostgresSql.Repositories.Roles.GetAllRoles;
using BookShop.PostgresSql.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.PostgresSql.Repositories.Roles.RemoveRoleTemporarilyById;

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
}
