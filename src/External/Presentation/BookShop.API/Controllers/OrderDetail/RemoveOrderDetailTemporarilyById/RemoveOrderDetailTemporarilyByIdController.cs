using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById;

[ApiController]
[Route(template: "api/order-detail/remove/temporarily")]
[Tags(tags: "OrderDetail")]
public class RemoveOrderDetailTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveOrderDetailTemporarilyByIdController(IMediator mediator)
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
    ///     DELETE api/order-detail/remove/temporarily
    ///
    /// </remarks>
    [HttpDelete("{order-detail-id}")]
    [ServiceFilter(typeof(RemoveOrderDetailTemporarilyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveOrderDetailTemporarilyByIdAsync(
        RemoveOrderDetailTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveOrderDetailTemporarilyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
