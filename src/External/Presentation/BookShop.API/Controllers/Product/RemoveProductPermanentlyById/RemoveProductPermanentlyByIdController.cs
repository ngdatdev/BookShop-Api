using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyById.HttpResponseMapper;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.RemoveProductPermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyById;

[ApiController]
[Route(template: "api/product/permanently")]
[Tags(tags: "Product")]
public class RemoveProductPermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveProductPermanentlyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing products permanently by id.
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
    ///     DELETE api/product/{product-id}
    ///
    /// </remarks>
    [HttpDelete("{product-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveProductPermanentlyByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RemoveProductPermanentlyByIdAuthorizationFilter), Order = 3)]
    public async Task<IActionResult> RemoveProductPermanentlyByIdAsync(
        RemoveProductPermanentlyByIdRequest removeProductTemporarilyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeProductTemporarilyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveProductPermanentlyByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeProductTemporarilyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
