using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.UpdateAddressById.HttpResponseMapper;
using BookShop.Application.Features.Addresses.UpdateAddressById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.UpdateAddressById;

[ApiController]
[Route(template: "api/address/")]
[Tags(tags: "Address")]
public class UpdateAddressByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateAddressByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating address by id.
    /// </summary>
    /// <param name="updateAddressByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/address/
    ///
    /// </remarks>
    [HttpPatch]
    public async Task<IActionResult> UpdateAddressByIdAsync(
        [FromBody] UpdateAddressByIdRequest updateAddressByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: updateAddressByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateAddressByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: updateAddressByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
