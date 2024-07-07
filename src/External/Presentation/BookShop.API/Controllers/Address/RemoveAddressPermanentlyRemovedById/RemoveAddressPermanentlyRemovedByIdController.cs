using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Address.RemoveAddressPermanentlyRemovedById.HttpResponseMapper;
using BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Address.RemoveAddressPermanentlyRemovedById;

[ApiController]
[Route(template: "api/address/permanently")]
[Tags(tags: "Address")]
public class RemoveAddressPermanentlyRemovedByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveAddressPermanentlyRemovedByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing address permanently by id.
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
    ///     DELETE api/address/permanently/{address-id}
    ///
    /// </remarks>
    [HttpDelete("{address-id}")]
    public async Task<IActionResult> RemoveAddressPermanentlyRemovedByIdAsync(
        [FromRoute] RemoveAddressPermanentlyRemovedByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveAddressPermanentlyRemovedByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
