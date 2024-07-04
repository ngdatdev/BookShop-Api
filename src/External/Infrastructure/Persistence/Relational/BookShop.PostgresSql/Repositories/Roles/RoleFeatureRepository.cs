using BookShop.Data.Features.Repositories.Roles;
using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Roles.CreateRole;

namespace BookShop.PostgresSql.Repositories.Roles;

/// <summary>
///    Implement of RoleFeatureRepository interface.
/// </summary>
internal class RoleFeatureRepository : IRoleFeatureRepository
{
    private readonly BookShopContext _context;
    private ICreateRoleRepository _createRoleRepository;

    internal RoleFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public ICreateRoleRepository CreateRoleRepository
    {
        get { return _createRoleRepository ??= new CreateRoleRepository(context: _context); }
    }
}
