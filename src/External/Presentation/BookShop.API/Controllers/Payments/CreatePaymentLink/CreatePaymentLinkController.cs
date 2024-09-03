using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.CreatePaymentLink.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Payments.CreatePaymentLink;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Payments.CreatePaymentLink;

[ApiController]
[Route(template: "api/payment/checkout-url")]
[Tags(tags: "Payment")]
public class CreatePaymentLinkController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreatePaymentLinkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for creating payment checkout url.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and checkout-url.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/payment/checkout-url
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<CreatePaymentLinkRequest>))]
    public async Task<IActionResult> CreatePaymentLinkAsync(
        [FromBody] CreatePaymentLinkRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = CreatePaymentLinkHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
