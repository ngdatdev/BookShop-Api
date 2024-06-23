using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductsByAuthorNameEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.Product.GetProductsByAuthorNameEndpoint.Middleware.Caching;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.GetProductsByAuthorName;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetProductsByAuthorName;

[ApiController]
[Route(template: "api/product/get-products-by-author")]
[Tags(tags: "Product")]
public class GetProductsByAuthorNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductsByAuthorNameController(
        IMediator mediator,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get products by author name.
    /// </summary>
    /// <param name="getProductsByAuthorNameRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response products information .
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/product/get-products-by-author?field1={fieldName1}?field1={fieldName1}
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetProductsByAuthorNameCachingFilter), Order = 2)]
    [ServiceFilter(typeof(ValidationRequestFilter<GetProductsByAuthorNameRequest>), Order = 1)]
    public async Task<IActionResult> GetProductsByAuthorNameAsync(
        [FromQuery] GetProductsByAuthorNameRequest getProductsByAuthorNameRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getProductsByAuthorNameRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetProductsByAuthorNameHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getProductsByAuthorNameRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
