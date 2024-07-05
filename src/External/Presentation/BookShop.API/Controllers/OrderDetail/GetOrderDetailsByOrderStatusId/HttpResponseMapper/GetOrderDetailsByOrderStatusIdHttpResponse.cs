using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.HttpResponseMapper;

/// <summary>
///     GetOrderDetailsByOrderStatusId http response
/// </summary>
internal sealed class GetOrderDetailsByOrderStatusIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetOrderDetailsByOrderStatusIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetOrderDetailsByOrderStatusIdResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
