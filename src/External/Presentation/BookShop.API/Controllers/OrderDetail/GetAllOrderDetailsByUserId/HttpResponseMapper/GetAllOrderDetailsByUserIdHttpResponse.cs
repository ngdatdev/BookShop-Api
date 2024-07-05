using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.GetAllOrderDetailsByUserId;

namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.HttpResponseMapper;

/// <summary>
///     GetAllOrderDetailsByUserId http response
/// </summary>
internal sealed class GetAllOrderDetailsByUserIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllOrderDetailsByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllOrderDetailsByUserIdResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
