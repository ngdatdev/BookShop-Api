﻿using BookShop.Application.Shared.Authentication.Jwt;
using BookShop.JsonWebToken.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.JsonWebToken;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void AddAppJwtIdentityService(this IServiceCollection services)
    {
        services
            .AddSingleton<IAccessTokenHandler, AccessTokenHandler>()
            .AddSingleton<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
