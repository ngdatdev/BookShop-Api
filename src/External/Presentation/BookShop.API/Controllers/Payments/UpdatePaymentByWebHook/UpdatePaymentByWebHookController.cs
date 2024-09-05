using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.UpdatePaymentByWebHook.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Payments.UpdatePaymentByWebHook;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Payments.UpdatePaymentByWebHook;

[ApiController]
[Route(template: "api/payment/webhook")]
[Tags(tags: "Payment")]
public class UpdatePaymentByWebHookController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdatePaymentByWebHookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating payment from webhook callback.
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
    ///     POST api/payment/webhook
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdatePaymentByWebHookRequest>))]
    public async Task<IActionResult> UpdatePaymentByWebHookAsync(
        [FromBody] UpdatePaymentByWebHookRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdatePaymentByWebHookHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
