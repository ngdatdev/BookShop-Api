using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;

namespace BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedOrder http response
/// </summary>
internal sealed class GetAllTemporarilyRemovedOrderHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllTemporarilyRemovedOrderResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllTemporarilyRemovedOrderResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
