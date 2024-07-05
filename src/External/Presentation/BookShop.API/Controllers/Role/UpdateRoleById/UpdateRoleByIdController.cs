using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.UpdateRoleById.HttpResponseMapper;
using BookShop.API.Controllers.Role.UpdateRoleById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Roles.UpdateRoleById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.UpdateRoleById;

[ApiController]
[Route(template: "api/role/update")]
[Tags(tags: "Role")]
public class UpdateRoleByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateRoleByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for temporarily removing role by id.
    /// </summary>
    /// <param name="createRoleRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/role/update
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(UpdateRoleByIdAuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdateRoleByIdRequest>))]
    public async Task<IActionResult> UpdateRoleByIdAsync(
        [FromBody] UpdateRoleByIdRequest createRoleRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateRoleByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
