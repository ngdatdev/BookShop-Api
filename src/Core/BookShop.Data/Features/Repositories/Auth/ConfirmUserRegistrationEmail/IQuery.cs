using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     Interface for Query ConfirmUserRegistrationEmail Repository
/// </summary>
public partial interface IConfirmUserRegistrationEmailRepository
{
    Task<bool> IsUserNotTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
