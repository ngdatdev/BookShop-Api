using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.API.HttpResponseMapper.ErrrorApiResponse;

namespace BookShop.API.Middleware;

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
                value: new ErrorHttpResponse
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    ErrorMessages = ["Server has encountered an error !", exception.Message]
                }
            );

            await httpContext.Response.CompleteAsync();
        }
    }
}
