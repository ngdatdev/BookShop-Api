using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.HttpResponseMapper;
using BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.Middleware.Authorization;
using BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.Middleware.Caching;
using BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById;

[ApiController]
[Route(template: "api/user/removing/all")]
[Tags(tags: "User")]
public class GetAllUsersTemporarilyRemovedByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllUsersTemporarilyRemovedByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all temporarily removed users.
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
    ///     GET api/user/removing/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllUsersTemporarilyRemovedByIdCachingFilter), Order = 2)]
    [ServiceFilter(typeof(GetAllUsersTemporarilyRemovedByIdAuthorizationFilter))]
    public async Task<IActionResult> GetAllUsersTemporarilyRemovedByIdAsync(
        [FromQuery] GetAllUsersTemporarilyRemovedByIdRequest getAllUsersRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getAllUsersRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllUsersTemporarilyRemovedByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getAllUsersRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
