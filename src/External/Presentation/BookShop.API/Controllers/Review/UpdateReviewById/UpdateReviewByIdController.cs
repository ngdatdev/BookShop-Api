using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.UpdateReviewById.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Reviews.UpdateReviewById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Review.UpdateReviewById;

[ApiController]
[Route(template: "api/review/updated")]
[Tags(tags: "Review")]
public class UpdateReviewByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateReviewByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for updating review by id.
    /// </summary>
    /// <param name="reviewId"></param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     App code.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/review/removed
    ///
    /// </remarks>
    [HttpPatch]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<UpdateReviewByIdRequest>))]
    public async Task<IActionResult> UpdateReviewByIdAsync(
        [FromRoute] Guid reviewId,
        CancellationToken cancellationToken
    )
    {
        UpdateReviewByIdRequest request = new() { ReviewId = reviewId };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = UpdateReviewByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
