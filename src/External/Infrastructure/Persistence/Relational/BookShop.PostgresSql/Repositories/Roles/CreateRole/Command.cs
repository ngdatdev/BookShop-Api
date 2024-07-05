using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.CreateRole;

/// <summary>
///    Implement of query ICreateRole repository.
/// </summary>
internal partial class CreateRoleRepository
{
    public async Task<bool> CreateRoleCommandAsync(
        Role newRole,
        RoleManager<Role> roleManager,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await roleManager.CreateAsync(role: newRole);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
