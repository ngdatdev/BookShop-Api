using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.RefreshAccessToken.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Auth.RefreshAccessToken;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.RefreshAccessToken;

[ApiController]
[Route(template: "api/auth/refresh-access-token")]
[Tags(tags: "Auth")]
public class RefreshAccessTokentController : ControllerBase
{
    private readonly IMediator _mediator;

    public RefreshAccessTokentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for refresh access token.
    /// </summary>
    /// <param name="refreshAccessTokenRequest">
    ///      Class contains refresh access token.
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
    ///     POST api/auth/refreshAccessToken
    ///     {
    ///         "RefreshToken": "string",
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(ValidationRequestFilter<RefreshAccessTokenRequest>))]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> RefreshAccessTokentAsync(
        [FromBody] RefreshAccessTokenRequest refreshAccessTokenRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: refreshAccessTokenRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RefreshAccessTokenHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: refreshAccessTokenRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
