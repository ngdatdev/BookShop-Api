using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.RestoreOrderDetailById.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.RestoreOrderDetailById.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.RestoreOrderDetailById;

[ApiController]
[Route(template: "api/order-detail/restore")]
[Tags(tags: "OrderDetail")]
public class RestoreOrderDetailByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreOrderDetailByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing temporarily order detail by id.
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
    ///     PATCH api/order-detail/restore
    ///
    /// </remarks>
    [HttpPatch("{order-detail-id}")]
    [ServiceFilter(typeof(RestoreOrderDetailByIdAuthorizationFilter))]
    public async Task<IActionResult> RestoreOrderDetailByIdAsync(
        RestoreOrderDetailByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreOrderDetailByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
