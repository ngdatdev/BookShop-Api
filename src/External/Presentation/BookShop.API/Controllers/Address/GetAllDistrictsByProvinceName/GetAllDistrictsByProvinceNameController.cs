using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.GetAllDistrictsByProvinceName.HttpResponseMapper;
using BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.GetAllDistrictsByProvinceName;

[ApiController]
[Route(template: "api/address/districts")]
[Tags(tags: "Address")]
public class GetAllDistrictsByProvinceNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllDistrictsByProvinceNameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting all districts by province name.
    /// </summary>
    /// <param name="province"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/address/districts/{ward}
    ///
    /// </remarks>
    [HttpGet("{province}")]
    public async Task<IActionResult> GetAllDistrictsByProvinceNameAsync(
        [FromRoute] [Required] string province,
        CancellationToken cancellationToken
    )
    {
        GetAllDistrictsByProvinceNameRequest request = new() { Province = province, };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllDistrictsByProvinceNameHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
