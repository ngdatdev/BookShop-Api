using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RemoveProductTemporarilyByIdByIdEndpoint.Middleware.Authorization;
using BookShop.API.Controllers.Product.RemoveProductTemporarilyByIdEndpoint.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.RemoveProductTemporarilyById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.RemoveProductTemporarilyById;

[ApiController]
[Route(template: "api/product/remove-temporarily")]
[Tags(tags: "Product")]
public class RemoveProductTemporarilyByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveProductTemporarilyByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for remove products temporarily by id.
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
    ///     DELETE api/product/remove-temporariy
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
