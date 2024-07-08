using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Cart.GetCartByUserId.HttpResponseMapper;
using BookShop.API.Controllers.Cart.GetCartByUserId.Middleware.Caching;
using BookShop.API.Controllers.CartItem.AddItemToCart.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Carts.GetCartByUserId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Cart.GetCartByUserId;

[ApiController]
[Route(template: "api/cart/user-id")]
[Tags(tags: "Cart")]
public class GetCartByUserIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetCartByUserIdController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting cart by user id information.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and class contains all cart information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/cart/user-id
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetCartByUserIdCachingFilter))]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> GetCartByUserIdAsync(CancellationToken cancellationToken)
    {
        GetCartByUserIdRequest getCartByUserIdRequest = new();
        var featureResponse = await _mediator.SendAsync(
            request: getCartByUserIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetCartByUserIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getCartByUserIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
