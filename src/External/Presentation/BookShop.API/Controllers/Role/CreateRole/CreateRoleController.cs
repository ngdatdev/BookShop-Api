using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Role.CreateRole.HttpResponseMapper;
using BookShop.API.Controllers.Role.CreateRole.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Roles.CreateRole;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Role.CreateRole;

[ApiController]
[Route(template: "api/role")]
[Tags(tags: "Role")]
public class CreateRoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateRoleController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for creating role.
    /// </summary>
    /// <param name="createRoleRequest">
    ///     Class contains adding role information.
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
    ///     POST api/role
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(CreateRoleAuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<CreateRoleRequest>))]
    public async Task<IActionResult> CreateRoleAsync(
        [FromBody] CreateRoleRequest createRoleRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: createRoleRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = CreateRoleHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createRoleRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
