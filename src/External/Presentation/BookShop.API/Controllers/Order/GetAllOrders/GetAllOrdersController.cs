using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.GetAllOrders.HttpResponseMapper;
using BookShop.API.Controllers.Order.GetAllOrders.Middleware.Authorization;
using BookShop.Application.Features.Orders.GetAllOrders;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.GetAllOrders;

[ApiController]
[Route(template: "api/order/all")]
[Tags(tags: "Order")]
public class GetAllOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all orders.
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
    ///     GET api/order/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllOrdersAuthorizationFilter))]
    public async Task<IActionResult> GetAllOrdersAsync(
        GetAllOrdersRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllOrdersHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
