using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.RemoveUserTemporarilyById.HttpResponseMapper;
using BookShop.API.Controllers.User.RemoveUserTemporarilyById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Users.RemoveUserTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.User.RemoveUserTemporarilyById;

[ApiController]
[Route(template: "api/user/temporarily")]
[Tags(tags: "User")]
public class RemoveUserTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveUserTemporarilyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for remove user temporarily by id.
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
    ///     DELETE api/user/temporarily/{user-id}
    ///
    /// </remarks>
    [HttpDelete("{user-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveUserTemporarilyByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RemoveUserTemporarilyByIdByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveUserTemporarilyByIdAsync(
        RemoveUserTemporarilyByIdRequest removeUserTemporarilyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeUserTemporarilyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveUserTemporarilyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeUserTemporarilyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
