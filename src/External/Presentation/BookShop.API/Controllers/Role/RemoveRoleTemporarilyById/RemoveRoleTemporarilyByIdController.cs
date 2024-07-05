using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.RemoveRoleTemporarilyById.HttpResponseMapper;
using BookShop.API.Controllers.Role.RemoveRoleTemporarilyById.Middleware.Authorization;
using BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.RemoveRoleTemporarilyById;

[ApiController]
[Route(template: "api/role/temporarily")]
[Tags(tags: "Role")]
public class RemoveRoleTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveRoleTemporarilyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for temporarily removing role by id.
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
    ///     DELETE api/role/temporarily/{role-id}
    ///
    /// </remarks>
    [HttpDelete("{role-id}")]
    [ServiceFilter(typeof(RemoveRoleTemporarilyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveRoleTemporarilyByIdAsync(
        [FromRoute(Name = "role-id")] Guid roleId,
        CancellationToken cancellationToken
    )
    {
        RemoveRoleTemporarilyByIdRequest createRoleRequest = new() { RoleId = roleId };

        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveRoleTemporarilyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
