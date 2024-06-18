using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Product.CreateProduct;

namespace BookShop.API.Controllers.Product.CreateProductEndpoint.HttpResponseMapper;

/// <summary>
///     CreateProduct http response
/// </summary>
internal sealed class CreateProductHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateProductResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
