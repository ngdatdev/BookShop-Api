using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.UpdateProductById.HttpResponseMapper;
using BookShop.API.Controllers.Product.UpdateProductById.Middleware.Authorization;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.UpdateProductById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.UpdateProductById;

[ApiController]
[Route(template: "api/product/update")]
[Tags(tags: "Product")]
public class UpdateProductByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductByIdController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating product.
    /// </summary>
    /// <param name="updateProductByIdRequest">
    ///     Class contains updating product request.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/product/update
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(UpdateProductByIdAuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdateProductByIdRequest>))]
    public async Task<IActionResult> UpdateProductByIdAsync(
        [FromForm] UpdateProductByIdRequest updateProductByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: updateProductByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateProductByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: updateProductByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
