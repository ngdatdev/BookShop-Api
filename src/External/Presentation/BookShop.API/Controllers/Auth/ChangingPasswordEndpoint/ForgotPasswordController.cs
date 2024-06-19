using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.ChangingPasswordEndpoint.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.ChangingPassword;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.ChangingPasswordEndpoint;

[ApiController]
[Route(template: "api/auth/changing-password")]
[Tags(tags: "Auth")]
public class ChangingPasswordController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChangingPasswordController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for changing password.
    /// </summary>
    /// <param name="changingPasswordRequest">
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
    ///     POST api/auth/changing-password
    ///     {
    ///         "NewPassword": "newpassword",
    ///         "ResetPasswordToken": "asdbyh3u1y234g1y31u2b3123123sdf"
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<ChangingPasswordRequest>), Order = 1)]
    public async Task<IActionResult> ChangingPasswordAsync(
        [FromBody] ChangingPasswordRequest changingPasswordRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: changingPasswordRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = ChangingPasswordHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: changingPasswordRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
