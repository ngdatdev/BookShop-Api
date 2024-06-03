using System;
using System.Threading.Tasks;
using BookShop.API.CommonResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.Middlewares;

/// <summary>
///     Global Exception Handler
/// </summary>
internal sealed class GlobalExceptionHandler
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.Clear(); // give a delete try
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                value: new ApiResponse
                {
                    AppCode = StatusCodes.Status500InternalServerError,
                    ErrorMessages = ["Server has encountered an error !", exception.Message]
                }
            );

            await httpContext.Response.CompleteAsync();
        }
    }
}
