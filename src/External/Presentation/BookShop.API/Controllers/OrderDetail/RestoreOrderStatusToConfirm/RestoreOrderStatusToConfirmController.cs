using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm;

[ApiController]
[Route(template: "api/order-detail/regain/order-status")]
[Tags(tags: "OrderDetail")]
public class RestoreOrderStatusToConfirmController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreOrderStatusToConfirmController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for regaining order status to confirmed order status.
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
    ///     PATCH api/order-detail/regain/order-status
    ///
    /// </remarks>
    [HttpPatch("{order-detail-id}")]
    [ServiceFilter(typeof(RestoreOrderStatusToConfirmAuthorizationFilter))]
    public async Task<IActionResult> RestoreOrderStatusToConfirmAsync(
        RestoreOrderStatusToConfirmRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreOrderStatusToConfirmHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
