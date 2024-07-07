using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

namespace BookShop.API.Controllers.Address.RemoveAddressPermanentlyRemovedById.HttpResponseMapper;

/// <summary>
///     RemoveAddressPermanentlyRemovedById http response
/// </summary>
internal sealed class RemoveAddressPermanentlyRemovedByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveAddressPermanentlyRemovedByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
