using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;

namespace BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProductsEndpoint.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedProducts http response
/// </summary>
internal sealed class GetAllTemporarilyRemovedProductsHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllTemporarilyRemovedProductsResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllTemporarilyRemovedProductsResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
