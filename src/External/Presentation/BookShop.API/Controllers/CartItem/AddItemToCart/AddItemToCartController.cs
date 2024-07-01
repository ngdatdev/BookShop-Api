using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.CartItem.AddItemToCart.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.CartItems.AddItemToCart;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.CartItem.AddItemToCart;

[ApiController]
[Route(template: "api/cart-item/add")]
[Tags(tags: "CartItem")]
public class AddItemToCartController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddItemToCartController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for adding item to cart information.
    /// </summary>
    /// <param name="getCartByUserIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/cart-item/add
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(AddItemToCartRequestValidator))]
    public async Task<IActionResult> AddItemToCartAsync(
        AddItemToCartRequest getCartByUserIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getCartByUserIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = AddItemToCartHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getCartByUserIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
