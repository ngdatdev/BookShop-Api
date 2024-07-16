using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.HttpResponseMapper;

/// <summary>
///     SwitchOrderStatusToNext http response
/// </summary>
internal sealed class SwitchOrderStatusToNextHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        SwitchOrderStatusToNextResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
