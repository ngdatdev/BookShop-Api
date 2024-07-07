using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.GetReviewsByProductId.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Reviews.GetReviewsByProductId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Review.GetReviewsByProductId;

[ApiController]
[Route(template: "api/review/product")]
[Tags(tags: "Review")]
public class GetReviewsByProductIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetReviewsByProductIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get reviews by product id.
    /// </summary>
    /// <param name="request">
    ///     Contain review information.
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
    ///     GET api/review/product/{product-id}
    ///
    /// </remarks>
    [HttpGet("{product-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<GetReviewsByProductIdRequest>))]
    public async Task<IActionResult> GetReviewsByProductIdAsync(
        GetReviewsByProductIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetReviewsByProductIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
