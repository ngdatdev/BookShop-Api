using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById.Middleware.Authorization;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById;

[ApiController]
[Route(template: "api/order-detail/remove/permanently")]
[Tags(tags: "OrderDetail")]
public class RemoveOrderDetailPermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveOrderDetailPermanentlyByIdController(IMediator mediator)
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
    ///     DELETE api/order-detail/remove/permanently
    ///
    /// </remarks>
    [HttpDelete("{order-detail-id}")]
    [ServiceFilter(typeof(RemoveOrderDetailPermanentlyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveOrderDetailPermanentlyByIdAsync(
        RemoveOrderDetailPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveOrderDetailPermanentlyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
