using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.CreateProductEndpoint.Middleware.Authorization;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.HelloWorld;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.HelloWorld;

[ApiController]
[Route("/api/hello-world")]
public sealed class HelloWorldController : ControllerBase
{
    private readonly IMediator _mediator;

    public HelloWorldController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for hello world.
    /// </summary>
    /// <param name="helloWorldRequest">
    ///     Class contains user credentials.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    ///     App code and some login information.
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/hello-world
    ///     {
    ///         "message": "Dat",
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<HelloWorldRequest>))]
    public async Task<IActionResult> HellWorld(
        [FromBody] HelloWorldRequest helloWorldRequest,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.SendAsync(
            request: helloWorldRequest,
            cancellationToken: cancellationToken
        );

        return Ok(response);
    }
}
