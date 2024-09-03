using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.GetAllPayments.HttpResponseMapper;
using BookShop.API.Controllers.Payments.GetAllPayments.Middleware.Authorization;
using BookShop.API.Controllers.Payments.GetAllPayments.Middleware.Caching;
using BookShop.Application.Features.Payments.GetAllPayments;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Payments.GetAllPayments;

[ApiController]
[Route(template: "api/payment/all")]
[Tags(tags: "Payment")]
public class GetAllPaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllPaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all payments.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and payments list.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/payment/all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllPaymentsAuthorizationFilter))]
    [ServiceFilter(typeof(GetAllPaymentsCachingFilter))]
    public async Task<IActionResult> GetAllPaymentsAsync(
        [FromQuery] GetAllPaymentsRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllPaymentsHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
