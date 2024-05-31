using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.DTOs.Response;

namespace BookShop.Application.Services.Interface;

/// <summary>
/// Interface for a service that handles operations related to user details.
/// </summary>
public interface IUserDetailService
{
    /// <summary>
    /// Retrieves all user details.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a collection of user detail responses.</returns>
    Task<IEnumerable<UserDetailResponse>> GetAllUserDetails(CancellationToken cancellationToken);
}
