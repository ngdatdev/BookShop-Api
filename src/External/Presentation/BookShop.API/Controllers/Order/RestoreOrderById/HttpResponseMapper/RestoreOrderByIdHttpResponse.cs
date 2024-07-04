using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Orders.RestoreOrderById;

namespace BookShop.API.Controllers.Order.RestoreOrderById.HttpResponseMapper;

/// <summary>
///     RestoreOrderById http response
/// </summary>
internal sealed class RestoreOrderByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RestoreOrderByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
