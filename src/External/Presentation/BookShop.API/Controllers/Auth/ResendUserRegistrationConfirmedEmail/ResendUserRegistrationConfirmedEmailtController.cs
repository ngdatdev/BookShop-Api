using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.ResendUserRegistrationConfirmedEmail.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.ResendUserRegistrationConfirmedEmail;

[ApiController]
[Route(template: "api/auth/sign-up/resend-email")]
[Tags(tags: "Auth")]
public class ResendUserRegistrationConfirmedEmailtController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResendUserRegistrationConfirmedEmailtController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for resend user register confirmed email.
    /// </summary>
    /// <param name="resendUserRegistrationConfirmedEmailRequest">
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
    ///     POST api/auth/resend-email
    ///     {
    ///         "username": "string",
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<ResendUserRegistrationConfirmedEmailRequest>))]
    public async Task<IActionResult> ResendUserRegistrationConfirmedEmailtAsync(
        [FromBody]
            ResendUserRegistrationConfirmedEmailRequest resendUserRegistrationConfirmedEmailRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: resendUserRegistrationConfirmedEmailRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = ResendUserRegistrationConfirmedEmailHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: resendUserRegistrationConfirmedEmailRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
