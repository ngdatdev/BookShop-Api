using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductsByCategoryId.HttpResponseMapper;
using BookShop.API.Controllers.Product.GetProductsByCategoryId.Middleware.Caching;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.GetProductsByCategoryId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetProductsByCategoryId;

[ApiController]
[Route(template: "api/product/category")]
[Tags(tags: "Product")]
public class GetProductsByCategoryIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductsByCategoryIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for getting products by categoryId information.
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
    ///     GET api/product/{category-id}?param={value}
    ///
    /// </remarks>
    [HttpGet("{category-id}")]
    [ServiceFilter(typeof(GetProductsByCategoryIdCachingFilter), Order = 2)]
    [ServiceFilter(typeof(ValidationRequestFilter<GetProductsByCategoryIdRequest>), Order = 1)]
    public async Task<IActionResult> GetProductsByCategoryIdAsync(
        GetProductsByCategoryIdRequest getProductsByCategoryIdRequest,
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
