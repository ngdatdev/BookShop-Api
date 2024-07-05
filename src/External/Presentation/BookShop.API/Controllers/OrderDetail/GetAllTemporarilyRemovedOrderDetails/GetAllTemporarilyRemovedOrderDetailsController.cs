using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.Middleware.Authorization;
using BookShop.API.Controllers.OrderDetail.GetAllTemporarilyRemovedOrderDetails.HttpResponseMapper;
using BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.GetAllTemporarilyRemovedOrderDetails;

[ApiController]
[Route(template: "api/order-detail/all/removed")]
[Tags(tags: "OrderDetail")]
public class GetAllTemporarilyRemovedOrderDetailsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTemporarilyRemovedOrderDetailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all temporarily removed order details.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response order information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/order-detail/all/removed
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllTemporarilyRemovedOrderAuthorizationFilter))]
    public async Task<IActionResult> GetAllTemporarilyRemovedOrderDetailsAsync(
        CancellationToken cancellationToken
    )
    {
        GetAllTemporarilyRemovedOrderDetailsRequest request = new();

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllTemporarilyRemovedOrderDetailsHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
