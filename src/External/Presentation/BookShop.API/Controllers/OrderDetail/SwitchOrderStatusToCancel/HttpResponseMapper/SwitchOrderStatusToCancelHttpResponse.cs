using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel.HttpResponseMapper;

/// <summary>
///     SwitchOrderStatusToCancel http response
/// </summary>
internal sealed class SwitchOrderStatusToCancelHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        SwitchOrderStatusToCancelResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
