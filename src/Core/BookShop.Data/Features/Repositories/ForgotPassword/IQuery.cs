using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.ForgotPassword;

/// <summary>
///     Interface for Command ForgotPasswordRepository Repository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
