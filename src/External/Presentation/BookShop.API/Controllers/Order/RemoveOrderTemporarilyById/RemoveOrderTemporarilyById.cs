using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.RemoveOrderTemporarilyById.HttpResponseMapper;
using BookShop.API.Controllers.Order.RemoveOrderTemporarilyById.Middleware.Authorization;
using BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.RemoveOrderTemporarilyById;

[ApiController]
[Route(template: "api/order/temporarily")]
[Tags(tags: "Order")]
public class RemoveOrderTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveOrderTemporarilyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing temporarily order by id.
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
    ///     DELETE api/order/temporarily
    ///
    /// </remarks>
    [HttpDelete("{order-id}")]
    [ServiceFilter(typeof(RemoveOrderTemporarilyByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveOrderTemporarilyByIdAsync(
        RemoveOrderTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveOrderTemporarilyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
