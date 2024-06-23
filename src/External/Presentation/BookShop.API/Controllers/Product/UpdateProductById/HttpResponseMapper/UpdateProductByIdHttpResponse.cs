using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Product.UpdateProductById;

namespace BookShop.API.Controllers.Product.UpdateProductByIdEndpoint.HttpResponseMapper;

/// <summary>
///     UpdateProductById http response
/// </summary>
internal sealed class UpdateProductByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateProductByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
