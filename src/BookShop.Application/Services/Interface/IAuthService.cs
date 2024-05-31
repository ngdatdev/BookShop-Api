using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.DTOs.Request;
using BookShop.Application.DTOs.Response;
using BookShop.Application.ResponseEntity;

namespace BookShop.Application.Services.Interface;

/// <summary>
/// Interface for a service that handles operations related to authentication.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Retrieves all user details.
    /// </summary>
    /// <param name="loginRequest">DTO contain information login</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A LoginResponse is cover by ResponseEntity</returns>
    Task<ResponseEntity<LoginResponse>> LoginAsync(
        LoginRequest loginRequest,
        CancellationToken cancellationToken
    );
}
