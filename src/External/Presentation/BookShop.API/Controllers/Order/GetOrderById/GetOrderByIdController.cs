using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.GetOrderById.HttpResponseMapper;
using BookShop.API.Controllers.Order.GetOrderById.Middleware.Caching;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Orders.GetOrderById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.GetOrderById;

[ApiController]
[Route(template: "api/order")]
[Tags(tags: "Order")]
public class GetOrderByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetOrderByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting order information by id.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response order information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order/{order-id}
    ///
    /// </remarks>
    [HttpGet("{order-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(GetOrderByIdCachingFilter))]
    public async Task<IActionResult> GetOrderByIdAsync(
        [FromRoute(Name = "order-id")] [Required] Guid orderId,
        CancellationToken cancellationToken
    )
    {
        GetOrderByIdRequest request = new() { OrderId = orderId };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetOrderByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
