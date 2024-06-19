using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.Common;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.GetProfileUserEndpoint.Middleware.Caching;

/// <summary>
///     Filter pipeline for get profile user caching.
/// </summary>
public class GetProfileUserCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetProfileUserCachingFilter(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        if (!context.HttpContext.Response.HasStarted)
        {
            var userId = context.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Sub
            );
            GetProfileUserStateBag.CacheKey = $"{nameof(GetProfileUserHttpResponse)}_{userId}";
            var cacheModel = await _cacheHandler.GetAsync<GetProfileUserHttpResponse>(
                key: GetProfileUserStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<GetProfileUserHttpResponse>.NotFound))
            {
                Console.WriteLine("CacheModel Data: ");
                Console.WriteLine(
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        cacheModel,
                        Newtonsoft.Json.Formatting.Indented
                    )
                );
                context.Result = new JsonResult(cacheModel.Value)
                {
                    StatusCode = cacheModel.Value.HttpCode,
                };
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (GetProfileUserHttpResponse)result.Value;
                await _cacheHandler.SetAsync(
                    key: GetProfileUserStateBag.CacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetProfileUserStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
