using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.UpdateUserById.HttpResponseMapper;
using BookShop.API.Controllers.User.UpdateUserById.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Users.UpdateUserById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.UpdateUserById;

[ApiController]
[Route(template: "api/user")]
[Tags(tags: "User")]
public class UpdateUserByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUserByIdController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating user profile.
    /// </summary>
    /// <param name="updateUserByIdRequest">
    ///     Class contains user information.
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
    ///     PATCH api/user
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(UpdateUserByIdCachingFilter), Order = 2)]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdateUserByIdRequest>), Order = 1)]
    public async Task<IActionResult> UpdateUserByIdAsync(
        [FromForm] UpdateUserByIdRequest updateUserByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: updateUserByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateUserByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: updateUserByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
