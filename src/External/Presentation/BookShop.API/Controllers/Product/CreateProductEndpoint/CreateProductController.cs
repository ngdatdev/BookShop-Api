using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.CreateProductEndpoint.HttpResponseMapper;
using BookShop.Application.Features.Product.CreateProduct;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.CreateProduct;

[ApiController]
[Route(template: "api/product/create")]
[Tags(tags: "Product")]
public class CreateProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateProductController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for create product information.
    /// </summary>
    /// <param name="getAllProductsRequest">
    ///     Class contains adding product information.
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
    ///     POST api/product/create
    ///
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromQuery] CreateProductRequest getAllProductsRequest,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: getAllProductsRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = CreateProductHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getAllProductsRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
