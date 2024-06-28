using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RestoreProductById.HttpResponseMapper;
using BookShop.API.Controllers.Product.RestoreProductById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.RestoreProductById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.RestoreProductById;

[ApiController]
[Route(template: "api/product/restore")]
[Tags(tags: "Product")]
public class RestoreProductByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestoreProductByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for resotre products temporarily by id.
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
    ///     PATCH api/product/{product-id}
    ///
    /// </remarks>
    [HttpPatch("{product-id}")]
    [ServiceFilter(typeof(ValidationRequestFilter<RestoreProductByIdRequest>), Order = 1)]
    [ServiceFilter(typeof(RestoreProductByIdAuthorizationFilter))]
    public async Task<IActionResult> RestoreProductByIdAsync(
        RestoreProductByIdRequest removeProductTemporarilyByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: removeProductTemporarilyByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = RestoreProductByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: removeProductTemporarilyByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
