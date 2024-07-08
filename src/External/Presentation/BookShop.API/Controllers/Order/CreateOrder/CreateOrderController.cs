using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.CreateOrder.HttpResponseMapper;
using BookShop.API.Controllers.Order.CreateOrder.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Orders.CreateOrder;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.CreateOrder;

[ApiController]
[Route(template: "api/order")]
[Tags(tags: "Order")]
public class CreateOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for creating order.
    /// </summary>
    /// <param name="createOrderRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/order
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<CreateOrderRequest>), Order = 1)]
    [ServiceFilter(typeof(CreateOrderCachingFilter), Order = 2)]
    public async Task<IActionResult> CreateOrderAsync(
        [FromBody] CreateOrderRequest createOrderRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: createOrderRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = CreateOrderHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: createOrderRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
