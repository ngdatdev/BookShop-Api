using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.GetAllRoles.HttpResponseMapper;
using BookShop.API.Controllers.Role.GetAllRoles.Middleware.Authorization;
using BookShop.Application.Features.Roles.GetAllRoles;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.GetAllRoles;

[ApiController]
[Route(template: "api/role/all")]
[Tags(tags: "Role")]
public class GetAllRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRolesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all roles.
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
    ///     GET api/role/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllRolesAuthorizationFilter))]
    public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        GetAllRolesRequest createRoleRequest = new();

        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllRolesHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
