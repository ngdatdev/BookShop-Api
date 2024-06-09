using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Shared.Filter.ControllerBase.ValidationFilter;
using BookShop.Application.Features.HelloWorld;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(policy: "VerifyAccessToken")]
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
}
