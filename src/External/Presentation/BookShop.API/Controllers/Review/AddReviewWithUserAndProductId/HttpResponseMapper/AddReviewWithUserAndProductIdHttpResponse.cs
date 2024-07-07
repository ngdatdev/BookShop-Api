using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

namespace BookShop.API.Controllers.Review.AddReviewWithUserAndProductId.HttpResponseMapper;

/// <summary>
///     AddReviewWithUserAndProductId http response
/// </summary>
internal sealed class AddReviewWithUserAndProductIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        AddReviewWithUserAndProductIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
