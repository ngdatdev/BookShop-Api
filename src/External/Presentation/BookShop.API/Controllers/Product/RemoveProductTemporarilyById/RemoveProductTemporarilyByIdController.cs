using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RemoveProductTemporarilyById.HttpResponseMapper;
using BookShop.API.Controllers.Product.RemoveProductTemporarilyById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.RemoveProductTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.RemoveProductTemporarilyById;

[ApiController]
[Route(template: "api/product/temporarily")]
[Tags(tags: "Product")]
public class RemoveProductTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveProductTemporarilyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing products temporarily by id.
    /// </summary>
    /// <param name="removeProductTemporarilyByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/product/temporarily/{product-id}
    ///
    /// </remarks>
    [HttpDelete("{product-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveProductTemporarilyByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RemoveProductTemporarilyByIdByIdAuthorizationFilter))]
    public async Task<IActionResult> RemoveProductTemporarilyByIdAsync(
        RemoveProductTemporarilyByIdRequest removeProductTemporarilyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeProductTemporarilyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveProductTemporarilyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeProductTemporarilyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
