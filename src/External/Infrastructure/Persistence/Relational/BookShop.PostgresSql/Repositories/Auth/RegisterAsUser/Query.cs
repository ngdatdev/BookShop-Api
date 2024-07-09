using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RegisterAsUser;

/// <summary>
///    Implement of query IRegisterAsUser repository.
/// </summary>
internal partial class RegisterAsUserRepository
{
    public Task<bool> IsUserFoundByNormalizedEmailQueryAsync(
        string email,
        CancellationToken cancellation
    )
    {
        email = email.ToUpper();

        return _users.AnyAsync(
            predicate: user => user.NormalizedEmail.Equals(email),
            cancellationToken: cancellation
        );
    }

    public Task<bool> IsUserFoundByNormalizedUsernameQueryAsync(
        string username,
        CancellationToken cancellation
    )
    {
        username = username.ToUpper();

        return _users.AnyAsync(
            predicate: user => user.NormalizedUserName.Equals(username),
            cancellationToken: cancellation
        );
    }
}
