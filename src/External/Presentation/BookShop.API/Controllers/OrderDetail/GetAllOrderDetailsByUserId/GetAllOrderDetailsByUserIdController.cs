using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.HttpResponseMapper;
using BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.Application.Features.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId;

[ApiController]
[Route(template: "api/order-detail/all/user")]
[Tags(tags: "OrderDetail")]
public class GetAllOrderDetailsByUserIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllOrderDetailsByUserIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all order details by user id.
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
    ///     GET api/order-detail/all/user
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(GetAllOrderDetailsByUserIdCachingFilter))]
    public async Task<IActionResult> GetAllOrderDetailsByUserIdAsync(
        CancellationToken cancellationToken
    )
    {
        GetAllOrderDetailsByUserIdRequest request = new();

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllOrderDetailsByUserIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
