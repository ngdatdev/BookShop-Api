using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.RemoveOrderPermanentlyById.HttpResponseMapper;
using BookShop.API.Controllers.Order.RemoveOrderPermanentlyById.Middleware.Authorization;
using BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.RemoveOrderPermanentlyById;

[ApiController]
[Route(template: "api/order/permanently")]
[Tags(tags: "Order")]
public class RemoveOrderPermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveOrderPermanentlyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing permanently order by id.
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
    ///     DELETE api/order/permanently
    ///
    /// </remarks>
    [HttpDelete("{order-id}")]
    [ServiceFilter(typeof(RemoveOrderPermanentlyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveOrderPermanentlyByIdAsync(
        RemoveOrderPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveOrderPermanentlyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
