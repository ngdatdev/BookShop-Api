using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LoginByGoogle.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.LoginByGoogle;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.LoginByGoogle;

[ApiController]
[Route(template: "api/auth/google")]
[Tags(tags: "Auth")]
public class LoginByGoogleController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginByGoogleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for user login through google.
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
    ///     POST api/auth/google
    ///     {
    ///         "IdToken": "string",
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<LoginByGoogleRequest>))]
    public async Task<IActionResult> LoginByGoogleAsync(
        [FromBody] LoginByGoogleRequest loginRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: loginRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = LoginByGoogleHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: loginRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
