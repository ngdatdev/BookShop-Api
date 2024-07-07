using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.RemoveReviewById.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Reviews.RemoveReviewById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Review.RemoveReviewById;

[ApiController]
[Route(template: "api/review/removed")]
[Tags(tags: "Review")]
public class RemoveReviewByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveReviewByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for removing review by id permanently.
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
    ///     DELETE api/review/removed
    ///
    /// </remarks>
    [HttpDelete]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<RemoveReviewByIdRequest>))]
    public async Task<IActionResult> RemoveReviewByIdAsync(
        [FromRoute] Guid reviewId,
        CancellationToken cancellationToken
    )
    {
        RemoveReviewByIdRequest request = new() { ReviewId = reviewId };

        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = RemoveReviewByIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
