using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.CartItem.UpdateCartItemById.HttpResponseMapper;
using BookShop.API.Controllers.CartItem.UpdateCartItemById.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.CartItems.UpdateCartItemById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.CartItem.UpdateCartItemById;

[ApiController]
[Route(template: "api/cart-item")]
[Tags(tags: "CartItem")]
public class UpdateCartItemByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateCartItemByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating quantity cart item.
    /// </summary>
    /// <param name="updateCartItemByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/cart-item
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdateCartItemByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(UpdateCartItemByIdCachingFilter), Order = 2)]
    public async Task<IActionResult> UpdateCartItemByIdAsync(
        [FromBody] UpdateCartItemByIdRequest updateCartItemByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: updateCartItemByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateCartItemByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: updateCartItemByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
