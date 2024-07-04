using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.GetOrdersByUserId.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.Orders.GetOrdersByUserId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Order.GetOrdersByUserId;

[ApiController]
[Route(template: "api/order/all/user-id")]
[Tags(tags: "Order")]
public class GetOrdersByUserIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetOrdersByUserIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all orders by user id.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response orders information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order/all/user-id
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public async Task<IActionResult> GetOrdersByUserIdAsync(CancellationToken cancellationToken)
    {
        GetOrdersByUserIdRequest request = new() { };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetOrdersByUserIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
