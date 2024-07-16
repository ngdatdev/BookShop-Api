using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext;

[ApiController]
[Route(template: "api/order-detail/switching/order-status")]
[Tags(tags: "OrderDetail")]
public class SwitchOrderStatusToNextController : ControllerBase
{
    private readonly IMediator _mediator;

    public SwitchOrderStatusToNextController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for switching order status to next order status.
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
    ///     PATCH api/order-detail/switching/order-status
    ///
    /// </remarks>
    [HttpPatch("{order-detail-id}")]
    [ServiceFilter(typeof(SwitchOrderStatusToNextAuthorizationFilter))]
    public async Task<IActionResult> SwitchOrderStatusToNextAsync(
        SwitchOrderStatusToNextRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = SwitchOrderStatusToNextHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
