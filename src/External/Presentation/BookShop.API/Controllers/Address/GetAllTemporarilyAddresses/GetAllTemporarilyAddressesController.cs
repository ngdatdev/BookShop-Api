using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.GetAllTemporarilyAddresses.HttpResponseMapper;
using BookShop.Application.Features.Addresses.GetAllTemporarilyAddresses;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Address.GetAllTemporarilyAddresses;

[ApiController]
[Route(template: "api/address/removing/all")]
[Tags(tags: "Address")]
public class GetAllTemporarilyAddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTemporarilyAddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get all temporarily adresses.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/address/removing/all
    ///
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetAllTemporarilyAddressesAsync(
        CancellationToken cancellationToken
    )
    {
        GetAllTemporarilyAddressesRequest request = new() { };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllTemporarilyAddressesHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
