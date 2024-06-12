using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.HelloWorld;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.HelloWorld;

    [ApiController]
    [Route("/api/[controller]")]
    public sealed class HelloWorldController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelloWorldController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Endpoint for user login.
        /// </summary>
        /// <param name="dto">
        ///     Class contains user credentials.
        /// </param>
        /// <param name="cancellationToken">
        ///     Automatic initialized token for aborting current operation.
        /// </param>
        ///     App code and some login information.
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Auth/sign-in
        ///     {
        ///         "email": "string",
        ///         "password": "string",
        ///         "rememberMe": true
        ///     }
        ///
        /// </remarks>
        [HttpPost("/hello")]
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

