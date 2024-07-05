using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

namespace BookShop.API.Controllers.OrderDetail.GetAllTemporarilyRemovedOrderDetails.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedOrderDetails http response
/// </summary>
internal sealed class GetAllTemporarilyRemovedOrderDetailsHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllTemporarilyRemovedOrderDetailsResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllTemporarilyRemovedOrderDetailsResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
