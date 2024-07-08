using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.RegisterAsUser.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.RegisterAsUser;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.RegisterAsUser;

[ApiController]
[Route(template: "api/auth/sign-up")]
[Tags(tags: "Auth")]
public class RegisterAsUsertController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterAsUsertController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for registering as user.
    /// </summary>
    /// <param name="registerAsUserRequest">
    ///      Class contains user register information.
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
    ///     POST api/auth/sign-up
    ///     {
    ///         "email": "string",
    ///         "username": "string",
    ///         "password": "string",
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<RegisterAsUserRequest>))]
    public async Task<IActionResult> RegisterAsUsertAsync(
        [FromBody] RegisterAsUserRequest registerAsUserRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: registerAsUserRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RegisterAsUserHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: registerAsUserRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
