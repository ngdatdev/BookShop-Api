using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderDetailTemporarilyById http response
/// </summary>
internal sealed class RemoveOrderDetailTemporarilyByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveOrderDetailTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
