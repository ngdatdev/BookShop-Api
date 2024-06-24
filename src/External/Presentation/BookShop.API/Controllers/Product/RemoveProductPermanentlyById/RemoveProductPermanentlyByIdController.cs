using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyByIdEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyByIdEndpoint.Middleware.Authorization;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyByIdEndpoint.Middleware.Caching;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.RemoveProductPermanentlyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyById;

[ApiController]
[Route(template: "api/product/remove-permanently")]
[Tags(tags: "Product")]
public class RemoveProductPermanentlyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveProductPermanentlyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for remove products permanently by id.
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
    ///     DELETE api/product/remove-permanently/{product-id}
    ///
    /// </remarks>
    [HttpDelete("{product-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveProductPermanentlyByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RemoveProductPermanentlyByIdCachingFilter), Order = 2)]
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
