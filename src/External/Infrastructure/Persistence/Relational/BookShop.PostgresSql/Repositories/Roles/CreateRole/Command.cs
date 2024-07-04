using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.PostgresSql.Repositories.Roles.CreateRole;

/// <summary>
///    Implement of query ICreateRole repository.
/// </summary>
internal partial class CreateRoleRepository
{
    public async Task<bool> CreateRoleCommandAsync(
        Role newRole,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _roles.AddAsync(entity: newRole);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
