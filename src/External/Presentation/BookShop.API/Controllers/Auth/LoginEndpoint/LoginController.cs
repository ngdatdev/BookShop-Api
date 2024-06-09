using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LoginEndpoint.HttpResponseMapper;
using BookShop.Application.Features.Auth.Login;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.LoginEndpoint;

[ApiController]
[Route(template: "api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for user login.
    /// </summary>
    /// <param name="loginRequest">
    ///     Class contains user credentials.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    ///     App code and some login information.
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Auth/sign-in
    ///     {
    ///         "username": "string",
    ///         "password": "string",
    ///         "rememberMe": true
    ///     }
    ///
    /// </remarks>
    [HttpPost(template: "sign-in")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginRequest loginRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: loginRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = LoginHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: loginRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
