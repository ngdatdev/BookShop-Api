using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.CartItems.UpdateCartItemById;

namespace BookShop.API.Controllers.CartItem.UpdateCartItemById.HttpResponseMapper;

/// <summary>
///     UpdateCartItemById http response
/// </summary>
internal sealed class UpdateCartItemByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateCartItemByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
