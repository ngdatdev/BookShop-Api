using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.SearchProductsByKeyword.HttpResponseMapper;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Product.SearchProductsByKeyword;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.SearchProductsByKeyword;

[ApiController]
[Route(template: "api/product/searching")]
[Tags(tags: "Product")]
public class SearchProductsByKeywordController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchProductsByKeywordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for searching products by keyword.
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
    ///     GET api/product/searching/?param={value}
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(ValidationRequestFilter<SearchProductsByKeywordRequest>), Order = 1)]
    public async Task<IActionResult> SearchProductsByKeywordAsync(
        SearchProductsByKeywordRequest getProductsByCategoryIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getProductsByCategoryIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = SearchProductsByKeywordHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getProductsByCategoryIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
