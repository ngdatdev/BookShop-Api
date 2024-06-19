using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Users.GetProfileUser;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.GetProfileUserEndpoint;

[ApiController]
[Route(template: "api/user/profile")]
[Tags(tags: "User")]
public class GetProfileUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProfileUserController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for user profile.
    /// </summary>
    /// <param name="getProfileUserRequest">
    ///     Class contains user credentials.
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
    ///     GET api/user/profile
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetProfileUserCachingFilter), Order = 2)]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> GetProfileUserAsync(CancellationToken cancellationToken)
    {
        var getProfileUserRequest = new GetProfileUserRequest();

        var featureResponse = await _mediator.SendAsync(
            request: getProfileUserRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetProfileUserHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getProfileUserRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
