using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.CartItem.RemoveCartItemById.HttpResponseMapper;
using BookShop.API.Controllers.CartItem.RemoveCartItemById.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.CartItems.RemoveCartItemById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.CartItem.RemoveCartItemById;

[ApiController]
[Route(template: "api/cart-item/remove")]
[Tags(tags: "CartItem")]
public class RemoveCartItemByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveCartItemByIdController(
        IMediator mediator,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing cart item by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/cart-item/remove/{id}
    ///
    /// </remarks>
    [HttpDelete("{id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveCartItemByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RemoveCartItemByIdCachingFilter), Order = 2)]
    public async Task<IActionResult> RemoveCartItemByIdAsync(
        [FromQuery] Guid id,
        CancellationToken cancellationToken
    )
    {
        RemoveCartItemByIdRequest getCartByUserIdRequest = new();

        getCartByUserIdRequest.CartItemId = id;

        var featureResponse = await _mediator.SendAsync(
            request: getCartByUserIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveCartItemByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getCartByUserIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
