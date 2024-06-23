using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.Middleware.Caching;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.GetProductsByCategoryId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetProductsByCategoryId;

[ApiController]
[Route(template: "api/product/get-products-by-category-id")]
[Tags(tags: "Product")]
public class GetProductsByCategoryIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductsByCategoryIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get products by categoryId.
    /// </summary>
    /// <param name="getProductsByCategoryIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/product/get-products-by-category-id/{categoryId}?field1={fieldName1}?field1={fieldName1}
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetProductsByCategoryIdCachingFilter), Order = 2)]
    [ServiceFilter(typeof(ValidationRequestFilter<GetProductsByCategoryIdRequest>), Order = 1)]
    public async Task<IActionResult> GetProductsByCategoryIdAsync(
        [FromQuery] GetProductsByCategoryIdRequest getProductsByCategoryIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getProductsByCategoryIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetProductsByCategoryIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getProductsByCategoryIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
