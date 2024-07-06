using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.GetAllWardsByDistrictName.HttpResponseMapper;
using BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.GetAllWardsByDistrictName;

[ApiController]
[Route(template: "api/address/wards")]
[Tags(tags: "Address")]
public class GetAllWardsByDistrictNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllWardsByDistrictNameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all wards by district name.
    /// </summary>
    /// <param name="district"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/address/wards/{district}
    ///
    /// </remarks>
    [HttpGet("{district}")]
    public async Task<IActionResult> GetAllWardsByDistrictNameAsync(
        [FromRoute] [Required] string district,
        CancellationToken cancellationToken
    )
    {
        GetAllWardsByDistrictNameRequest request = new() { District = district, };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllWardsByDistrictNameHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
