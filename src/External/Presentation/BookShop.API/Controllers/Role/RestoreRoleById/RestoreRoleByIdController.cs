using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.RestoreRoleById.HttpResponseMapper;
using BookShop.API.Controllers.Role.RestoreRoleById.Middleware.Authorization;
using BookShop.Application.Features.Roles.RestoreRoleById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.RestoreRoleById;

[ApiController]
[Route(template: "api/role/restore")]
[Tags(tags: "Role")]
public class RestoreRoleByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreRoleByIdController(IMediator mediator)
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
    ///     PATCH api/role/restore/{role-id}
    ///
    /// </remarks>
    [HttpPatch("{role-id}")]
    [ServiceFilter(typeof(RestoreRoleByIdAuthorizationFilter))]
    public async Task<IActionResult> RestoreRoleByIdAsync(
        [FromRoute(Name = "role-id")] Guid roleId,
        CancellationToken cancellationToken
    )
    {
        RestoreRoleByIdRequest createRoleRequest = new() { RoleId = roleId };

        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreRoleByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
