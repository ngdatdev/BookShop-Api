using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Data.Features.Repositories.Auth.RegisterAsUser;

/// <summary>
///     Interface for Command RegisterAsUserRepository Repository
/// </summary>
public partial interface IRegisterAsUserRepository
{
    Task<bool> CreateUserAndAddUserRoleCommandAsync(
        Shared.Entities.User newUser,
        string userPassword,
        UserManager<Shared.Entities.User> userManager,
        string userRole,
        Cart newCart,
        CancellationToken cancellationToken
    );
}
