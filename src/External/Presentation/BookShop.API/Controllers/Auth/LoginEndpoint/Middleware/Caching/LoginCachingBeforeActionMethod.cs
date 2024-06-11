using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LoginEndpoint.Common;
using BookShop.API.Controllers.Auth.LoginEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Controllers.Auth.LoginEndpoint.Middleware.Caching;

/// <summary>
///     Pre-processor for login caching.
/// </summary>
public class LoginCachingBeforeActionMethod : IAsyncActionFilter
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LoginCachingBeforeActionMethod(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var httpContext = context.HttpContext;
        if (!context.HttpContext.Response.HasStarted)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var cacheHandler = scope.ServiceProvider.GetRequiredService<ICacheHandler>();

            LoginStateBag.CacheKey = $"{nameof(LoginHttpResponse)}";
            var cacheModel = await cacheHandler.GetAsync<LoginHttpResponse>(
                key: LoginStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if(!Equals(objA: cacheModel, objB: AppCacheModel<LoginHttpResponse>.NotFound)) {
                
            }
        }
    }
}
