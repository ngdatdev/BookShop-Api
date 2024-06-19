using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LogoutEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.Product.CreateProductEndpoint.Middleware.Authorization;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Auth.Logout;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.LogoutEndpoint;

[ApiController]
[Route(template: "api/auth/logout")]
[Tags(tags: "Auth")]
public class LogoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogoutController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for user logout.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/auth/logout
    ///
    /// </remarks>
    [HttpDelete]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
    {
        LogoutRequest logoutRequest = new() { };
        var featureResponse = await _mediator.SendAsync(
            request: logoutRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = LogoutHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: logoutRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
