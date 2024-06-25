using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductById.HttpResponseMapper;
using BookShop.API.Controllers.Product.GetProductById.Middleware.Caching;
using BookShop.Application.Features.Product.GetProductById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetProductById;

[ApiController]
[Route(template: "api/product")]
[Tags(tags: "Product")]
public class GetProductByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get product by id information.
    /// </summary>
    /// <param name="getProductByIdRequest"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and response product information .
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/product/{product-id}
    ///
    /// </remarks>
    [HttpGet("{product-id}")]
    [ServiceFilter(typeof(GetProductByIdCachingFilter), Order = 2)]
    public async Task<IActionResult> GetProductByIdAsync(
        GetProductByIdRequest getProductByIdRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getProductByIdRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetProductByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getProductByIdRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
