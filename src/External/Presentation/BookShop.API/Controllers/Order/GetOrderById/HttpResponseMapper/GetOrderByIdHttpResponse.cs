using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Orders.GetOrderById;

namespace BookShop.API.Controllers.Order.GetOrderById.HttpResponseMapper;

/// <summary>
///     GetOrderById http response
/// </summary>
internal sealed class GetOrderByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetOrderByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetOrderByIdResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
