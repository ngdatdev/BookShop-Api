using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.GetReviewsByUserId.HttpResponseMapper;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using BookShop.Application.Features.Reviews.GetReviewsByUserId;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.Review.GetReviewsByUserId;

[ApiController]
[Route(template: "api/review/user")]
[Tags(tags: "Review")]
public class GetReviewsByUserIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetReviewsByUserIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Endpoint for get reviews by user id.
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
    ///     GET api/review/user{user-id}
    ///
    /// </remarks>
    [HttpGet("{user-id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(ValidationRequestFilter<GetReviewsByUserIdRequest>))]
    public async Task<IActionResult> GetReviewsByUserIdAsync(
        GetReviewsByUserIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var featureResponse = await _mediator.SendAsync(
            request: request,
            cancellationToken: cancellationToken
        );

        var apiResponse = GetReviewsByUserIdHttpResponseMapper
            .Get()
            .Resolve(featureResponse.StatusCode)
            .Invoke(arg1: request, featureResponse);

        return StatusCode(statusCode: apiResponse.HttpCode, value: apiResponse);
    }
}
