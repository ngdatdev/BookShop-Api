using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.RemoveAddressTemporarilyRemovedById.HttpResponseMapper;
using BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Auth.RemoveAddressTemporarilyRemovedById;

[ApiController]
[Route(template: "api/address/temporarily")]
[Tags(tags: "Address")]
public class RemoveAddressTemporarilyRemovedByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveAddressTemporarilyRemovedByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing address temporarily by id.
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
    ///     DELETE api/address/temporarily/{address-id}
    ///
    /// </remarks>
    [HttpDelete(Name = "{address-id}")]
    public async Task<IActionResult> RemoveAddressTemporarilyRemovedByIdAsync(
        [FromRoute] RemoveAddressTemporarilyRemovedByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveAddressTemporarilyRemovedByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
