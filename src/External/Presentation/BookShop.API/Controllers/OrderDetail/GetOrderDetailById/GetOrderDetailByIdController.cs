using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.OrderDetails.GetOrderDetailById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById;

[ApiController]
[Route(template: "api/order-detail")]
[Tags(tags: "OrderDetail")]
public class GetOrderDetailByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetOrderDetailByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting order detail by user id and order detail id.
    /// </summary>
    /// <param name="orderDetailId"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response order information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order-detail/{order-detail-id}
    ///
    /// </remarks>
    [HttpGet("{order-detail-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(GetOrderDetailByIdCachingFilter))]
    public async Task<IActionResult> GetOrderDetailByIdAsync(
        [FromRoute(Name = "order-detail-id")] [Required] Guid orderDetailId,
        CancellationToken cancellationToken
    )
    {
        GetOrderDetailByIdRequest request = new() { OrderDetailId = orderDetailId };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetOrderDetailByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
