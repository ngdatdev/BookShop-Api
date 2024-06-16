using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.ForgotPassword;

/// <summary>
///     Interface for Command ForgotPasswordRepository Repository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
