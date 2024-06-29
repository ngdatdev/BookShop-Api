using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.ConfirmUserRegistrationEmail.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.ConfirmUserRegistrationEmail;

[ApiController]
[Route(template: "api/auth/sign-up/confirm-email")]
[Tags(tags: "Auth")]
public class ConfirmUserRegistrationEmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfirmUserRegistrationEmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for confirm user registration confirmed email.
    /// </summary>
    /// <param name="base64UserRegistrationConfirmedEmailToken">
    ///     A token value that will be used for user registration
    ///     email confirmation
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
    ///     GET api/auth/sign-up/confirm-email?
    ///         token={base64UserRegistrationConfirmedEmailToken}
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(ValidationRequestFilter<ConfirmUserRegistrationEmailRequest>), Order = 1)]
    public async Task<IActionResult> ConfirmUserRegistrationEmailAsync(
        [FromQuery(Name = "token")] [Required] string base64UserRegistrationConfirmedEmailToken,
        CancellationToken cancellationToken
    )
    {
        ConfirmUserRegistrationEmailRequest requestFeature =
            new()
            {
                UserRegistrationEmailConfirmedTokenAsBase64 =
                    base64UserRegistrationConfirmedEmailToken,
            };

        var featureResponse = await _mediator.SendAsync(
            request: requestFeature,
            cancellationToken: cancellationToken
        );

        var apiResponse = ConfirmUserRegistrationEmailHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: requestFeature, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
