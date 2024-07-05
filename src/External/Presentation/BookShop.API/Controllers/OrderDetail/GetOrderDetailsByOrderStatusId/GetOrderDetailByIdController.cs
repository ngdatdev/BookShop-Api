using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId;

[ApiController]
[Route(template: "api/order-detail/all")]
[Tags(tags: "OrderDetail")]
public class GetOrderDetailsByOrderStatusIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetOrderDetailsByOrderStatusIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all order details by user id and order status id.
    /// </summary>
    /// <param name="orderStatusId"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response order information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order-detail/all/{order-status-id}
    ///
    /// </remarks>
    [HttpGet("{order-status-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(GetOrderDetailsByOrderStatusIdCachingFilter))]
    public async Task<IActionResult> GetOrderDetailsByOrderStatusIdAsync(
        [FromRoute(Name = "order-status-id")] [Required] Guid orderStatusId,
        CancellationToken cancellationToken
    )
    {
        GetOrderDetailsByOrderStatusIdRequest request = new() { OrderStatusId = orderStatusId };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetOrderDetailsByOrderStatusIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
