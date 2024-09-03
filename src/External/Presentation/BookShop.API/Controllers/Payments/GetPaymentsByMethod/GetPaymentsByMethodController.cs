using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.GetPaymentsByMethod.HttpResponseMapper;
using BookShop.API.Controllers.Payments.GetPaymentsByMethod.Middleware.Authorization;
using BookShop.API.Controllers.Payments.GetPaymentsByMethod.Middleware.Caching;
using BookShop.Application.Features.Payments.GetPaymentsByMethod;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Payments.GetPaymentsByMethod;

[ApiController]
[Route(template: "api/payment/method")]
[Tags(tags: "Payment")]
public class GetPaymentsByMethodController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetPaymentsByMethodController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting payments by method.
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
    ///     GET api/payment/method/{method}
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetPaymentsByMethodAuthorizationFilter))]
    [ServiceFilter(typeof(GetPaymentsByMethodCachingFilter))]
    public async Task<IActionResult> GetPaymentsByMethodAsync(
        GetPaymentsByMethodRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetPaymentsByMethodHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
