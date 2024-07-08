using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.RemoveRolePermanentlyById.HttpResponseMapper;
using BookShop.API.Controllers.Role.RemoveRolePermanentlyById.Middleware.Authorization;
using BookShop.Application.Features.Roles.RemoveRolePermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.RemoveRolePermanentlyById;

[ApiController]
[Route(template: "api/role/permanently")]
[Tags(tags: "Role")]
public class RemoveRolePermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveRolePermanentlyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing permanently role by id.
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/role/permanently/{role-id}
    ///
    /// </remarks>
    [HttpDelete("{role-id}")]
    [ServiceFilter(typeof(RemoveRolePermanentlyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveRolePermanentlyByIdAsync(
        [FromRoute(Name = "role-id")] Guid roleId,
        CancellationToken cancellationToken
    )
    {
        RemoveRolePermanentlyByIdRequest createRoleRequest = new() { RoleId = roleId };

        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveRolePermanentlyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
