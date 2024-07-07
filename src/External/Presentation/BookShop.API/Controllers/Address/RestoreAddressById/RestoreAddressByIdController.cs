using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.RestoreAddressById.HttpResponseMapper;
using BookShop.Application.Features.Addresses.RestoreAddressById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Address.RestoreAddressById;

[ApiController]
[Route(template: "api/address/restore")]
[Tags(tags: "Address")]
public class RestoreAddressByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreAddressByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for restoring address by id.
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
    ///     PATCH api/address/restore/{address-id}
    ///
    /// </remarks>
    [HttpPatch("{address-id}")]
    public async Task<IActionResult> RestoreAddressByIdAsync(
        [FromRoute] RestoreAddressByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreAddressByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
