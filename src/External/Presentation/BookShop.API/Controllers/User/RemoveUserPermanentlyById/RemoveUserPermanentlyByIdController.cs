using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.RemoveUserPermanentlyById.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Users.RemoveUserPermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.RemoveUserPermanentlyById;

[ApiController]
[Route(template: "api/user/permanently")]
[Tags(tags: "User")]
public class RemoveUserPermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveUserPermanentlyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing user permanently by id.
    /// </summary>
    /// <param name="removeUserPermanentlyByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/user/permanently/{user-id}
    ///
    /// </remarks>
    [HttpDelete("{user-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> RemoveUserPermanentlyByIdAsync(
        RemoveUserPermanentlyByIdRequest removeUserPermanentlyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeUserPermanentlyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveUserPermanentlyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeUserPermanentlyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
