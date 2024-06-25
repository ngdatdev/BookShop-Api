using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetAllUsers.HttpResponseMapper;
using BookShop.API.Controllers.User.GetAllUsers.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Users.GetAllUsers;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.GetAllUsers;

[ApiController]
[Route(template: "api/user/all")]
[Tags(tags: "User")]
public class GetAllUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllUsersController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get all users.
    /// </summary>
    /// <param name="getAllUsersRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and users response.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/user/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllUsersCachingFilter), Order = 2)]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> GetAllUsersAsync(
        [FromQuery] GetAllUsersRequest getAllUsersRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getAllUsersRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllUsersHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getAllUsersRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
