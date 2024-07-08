using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Cart.ClearCart.HttpResponseMapper;
using BookShop.API.Controllers.Cart.ClearCart.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Carts.ClearCart;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Cart.ClearCart;

[ApiController]
[Route(template: "api/cart/clearing")]
[Tags(tags: "Cart")]
public class ClearCartController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClearCartController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for clearing all item in cart.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/cart/clear
    ///
    /// </remarks>
    [HttpDelete]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ClearCartCachingFilter))]
    public async Task<IActionResult> ClearCartAsync(CancellationToken cancellationToken)
    {
        var clearCartRequest = new ClearCartRequest();

        var featureResponse = await _mediator.SendAsync(
            request: clearCartRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = ClearCartHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: clearCartRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
