using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.UpdatePaymentCOD.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Payments.UpdatePaymentCOD;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Payments.UpdatePaymentCOD;

[ApiController]
[Route(template: "api/payment/cod")]
[Tags(tags: "Payment")]
public class UpdatePaymentCODController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdatePaymentCODController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating payment cod cash.
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
    ///     PATCH api/payment/cod
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdatePaymentCODRequest>))]
    public async Task<IActionResult> UpdatePaymentCODAsync(
        [FromBody] UpdatePaymentCODRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdatePaymentCODHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
