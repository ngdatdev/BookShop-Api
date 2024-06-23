using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Product.GetProductsByAuthorName;

namespace BookShop.API.Controllers.Product.GetProductsByAuthorNameEndpoint.HttpResponseMapper;

/// <summary>
///     GetProductsByAuthorName http response
/// </summary>
internal sealed class GetProductsByAuthorNameHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetProductsByAuthorNameResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetProductsByAuthorNameResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
