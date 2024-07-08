using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.RestoreUserById.HttpResponseMapper;
using BookShop.API.Controllers.User.RestoreUserById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Users.RestoreUserById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.User.RestoreUserById;

[ApiController]
[Route(template: "api/user/restore")]
[Tags(tags: "User")]
public class RestoreUserByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreUserByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for restoring user temporarily by id.
    /// </summary>
    /// <param name="removeUserTemporarilyByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/user/restore/{user-id}
    ///
    /// </remarks>
    [HttpPatch("{user-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RestoreUserByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RestoreUserByIdAuthorizationFilter))]
    public async Task<IActionResult> RestoreUserByIdAsync(
        RestoreUserByIdRequest removeUserTemporarilyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeUserTemporarilyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreUserByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeUserTemporarilyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
