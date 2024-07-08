using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.GetAddressesByWard.HttpResponseMapper;
using BookShop.Application.Features.Addresses.GetAddressesByWard;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Address.GetAddressesByWard;

[ApiController]
[Route(template: "api/address/all")]
[Tags(tags: "Address")]
public class GetAddressesByWardController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAddressesByWardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all adresses by ward name.
    /// </summary>
    /// <param name="ward"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/address/all/{ward}
    ///
    /// </remarks>
    [HttpGet("{ward}")]
    public async Task<IActionResult> GetAddressesByWardAsync(
        [FromRoute] [Required] string ward,
        CancellationToken cancellationToken
    )
    {
        GetAddressesByWardRequest request = new() { Ward = ward, };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAddressesByWardHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
