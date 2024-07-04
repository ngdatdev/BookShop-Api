using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.RestoreOrderById.HttpResponseMapper;
using BookShop.API.Controllers.Order.RestoreOrderById.Middleware.Authorization;
using BookShop.Application.Features.Orders.RestoreOrderById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.RestoreOrderById;

[ApiController]
[Route(template: "api/order/restore")]
[Tags(tags: "Order")]
public class RestoreOrderByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreOrderByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for restoring order by id.
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
    ///     PATCH api/order/restore/{order-id}
    ///
    /// </remarks>
    [HttpPatch("{order-id}")]
    [ServiceFilter(typeof(RestoreOrderByIdAuthorizationFilter))]
    public async Task<IActionResult> RestoreOrderByIdAsync(
        RestoreOrderByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreOrderByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
