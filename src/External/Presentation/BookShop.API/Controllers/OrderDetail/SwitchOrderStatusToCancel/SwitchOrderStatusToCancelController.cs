using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel;

[ApiController]
[Route(template: "api/order-detail/cancel/order-status")]
[Tags(tags: "OrderDetail")]
public class SwitchOrderStatusToCancelController : ControllerBase
{
    private readonly IMediator _mediator;

    public SwitchOrderStatusToCancelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for switching order status to cancel order status.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/order-detail/cancel/order-status
    ///
    /// </remarks>
    [HttpPatch("{order-detail-id}")]
    [ServiceFilter(typeof(SwitchOrderStatusToCancelAuthorizationFilter))]
    public async Task<IActionResult> SwitchOrderStatusToCancelAsync(
        SwitchOrderStatusToCancelRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = SwitchOrderStatusToCancelHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
