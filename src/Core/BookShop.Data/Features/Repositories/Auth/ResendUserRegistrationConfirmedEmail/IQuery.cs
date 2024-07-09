using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     Interface for Query ResendUserRegistrationConfirmedEmai Repository
/// </summary>
public partial interface IResendUserRegistrationConfirmedEmailRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
