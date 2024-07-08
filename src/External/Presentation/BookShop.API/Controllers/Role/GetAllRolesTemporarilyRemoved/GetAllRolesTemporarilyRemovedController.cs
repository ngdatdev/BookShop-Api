using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved.HttpResponseMapper;
using BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved.Middleware.Authorization;
using BookShop.Application.Features.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved;

[ApiController]
[Route(template: "api/role/removing/all")]
[Tags(tags: "Role")]
public class GetAllRolesTemporarilyRemovedController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRolesTemporarilyRemovedController(
        IMediator mediator,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all roles temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and roles information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/role/removing/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllRolesTemporarilyRemovedAuthorizationFilter))]
    public async Task<IActionResult> GetAllRolesTemporarilyRemovedAsync(
        CancellationToken cancellationToken
    )
    {
        GetAllRolesTemporarilyRemovedRequest createRoleRequest = new();

        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllRolesTemporarilyRemovedHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
