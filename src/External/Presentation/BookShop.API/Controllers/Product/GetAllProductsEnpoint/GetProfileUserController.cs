using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetAllProductsEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.User.GetAllProductsEndpoint.Middleware.Caching;
using BookShop.Application.Features.Product.GetAllProducts;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetAllProducts;

[ApiController]
[Route(template: "api/product/get-all")]
[Tags(tags: "Product")]
public class GetAllProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllProductsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for all product information.
    /// </summary>
    /// <param name="getAllProductsRequest">
    ///     Class contains all products information.
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
    ///     GET api/product/get-all
    ///
    /// </remarks>
    [HttpGet]
    [ServiceFilter(typeof(GetAllProductsCachingFilter))]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var getAllProductsRequest = new GetAllProductsRequest();

        var featureResponse = await _mediator.SendAsync(
            request: getAllProductsRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllProductsHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getAllProductsRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
