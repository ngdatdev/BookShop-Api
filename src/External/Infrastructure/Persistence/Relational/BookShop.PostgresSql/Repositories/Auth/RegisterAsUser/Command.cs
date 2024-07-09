using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RegisterAsUser;

/// <summary>
///    Implement of command IRegisterAsUser repository.
/// </summary>
internal partial class RegisterAsUserRepository
{
    public async Task<bool> CreateUserAndAddUserRoleCommandAsync(
        BookShop.Data.Shared.Entities.User newUser,
        string userPassword,
        UserManager<BookShop.Data.Shared.Entities.User> userManager,
        string userRole,
        Cart newCart,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    var result = await userManager.CreateAsync(
                        user: newUser,
                        password: userPassword
                    );

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    result = await userManager.AddToRoleAsync(user: newUser, role: userRole);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    await _carts.AddAsync(entity: newCart);

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
    }
}
