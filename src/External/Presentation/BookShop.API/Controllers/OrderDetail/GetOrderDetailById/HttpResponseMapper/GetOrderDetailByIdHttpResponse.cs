using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.GetOrderDetailById;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;

/// <summary>
///     GetOrderDetailById http response
/// </summary>
internal sealed class GetOrderDetailByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetOrderDetailByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetOrderDetailByIdResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
