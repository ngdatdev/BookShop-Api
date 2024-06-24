using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProductsEndpoint.HttpResponseMapper;
using BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProducts;

[ApiController]
[Route(template: "api/product/remove/all")]
[Tags(tags: "Product")]
public class GetAllTemporarilyRemovedProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTemporarilyRemovedProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for all product information.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code and class contains all products information.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/product/get-all
    ///
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetAllTemporarilyRemovedProductsAsync(
        CancellationToken cancellationToken
    )
    {
        GetAllTemporarilyRemovedProductsRequest getAllProductsRequest = new();

        var featureResponse = await _mediator.SendAsync(
            request: getAllProductsRequest,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetAllTemporarilyRemovedProductsHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: getAllProductsRequest, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
