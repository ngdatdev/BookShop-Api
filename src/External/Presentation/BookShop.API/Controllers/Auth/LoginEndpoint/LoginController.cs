using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LoginEndpoint.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.Login;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.LoginEndpoint;

[ApiController]
[Route(template: "api/auth/login")]
[Tags(tags: "Auth")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
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
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/auth/login
    ///     {
    ///         "username": "username",
    ///         "password": "Username123@",
    ///         "rememberMe": true
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<LoginRequest>))]
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
