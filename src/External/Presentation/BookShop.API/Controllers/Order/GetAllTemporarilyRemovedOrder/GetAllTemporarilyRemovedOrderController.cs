using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.HttpResponseMapper;
using BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProducts.Middleware.Authorization;
using BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder;

[ApiController]
[Route(template: "api/order/removed/all")]
[Tags(tags: "Order")]
public class GetAllTemporarilyRemovedOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTemporarilyRemovedOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all temprorarily removed orders.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response orders information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order/removed/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllTemporarilyRemovedProductsAuthorizationFilter))]
    public async Task<IActionResult> GetAllTemporarilyRemovedOrderAsync(
        GetAllTemporarilyRemovedOrderRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllTemporarilyRemovedOrderHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
