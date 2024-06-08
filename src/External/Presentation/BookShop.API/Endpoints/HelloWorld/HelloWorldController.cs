using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Endpoints.HelloWorld.Middleware.Validation;
using BookShop.Application.Features.HelloWorld;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Endpoints.HelloWorld
{
    [ApiController]
    [Route("/a")]
    public class HelloWorldController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelloWorldController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/hello")]
        [ServiceFilter(typeof(HelloWorldValidationFilter))]
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
}
