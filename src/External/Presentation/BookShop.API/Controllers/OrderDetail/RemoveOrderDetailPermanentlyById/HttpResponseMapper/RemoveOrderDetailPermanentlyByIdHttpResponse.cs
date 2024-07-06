using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderDetailPermanentlyById http response
/// </summary>
internal sealed class RemoveOrderDetailPermanentlyByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveOrderDetailPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
