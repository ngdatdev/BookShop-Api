using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.AddReviewWithUserAndProductId.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Review.AddReviewWithUserAndProductId;

[ApiController]
[Route(template: "api/review/create")]
[Tags(tags: "Review")]
public class AddReviewWithUserAndProductIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddReviewWithUserAndProductIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for adding review product with user id and product id.
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
    ///     POST api/review/create
    ///
    /// </remarks>
    [HttpPost]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<AddReviewWithUserAndProductIdRequest>))]
    public async Task<IActionResult> AddReviewWithUserAndProductIdAsync(
        [FromBody] AddReviewWithUserAndProductIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = AddReviewWithUserAndProductIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
