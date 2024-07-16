using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

namespace BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm.HttpResponseMapper;

/// <summary>
///     RestoreOrderStatusToConfirm http response
/// </summary>
internal sealed class RestoreOrderStatusToConfirmHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RestoreOrderStatusToConfirmResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
