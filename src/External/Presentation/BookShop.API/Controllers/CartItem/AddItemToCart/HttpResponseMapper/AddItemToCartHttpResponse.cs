using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.CartItems.AddItemToCart;

namespace BookShop.API.Controllers.CartItem.AddItemToCart.HttpResponseMapper;

/// <summary>
///     AddItemToCart http response
/// </summary>
internal sealed class AddItemToCartHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        AddItemToCartResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
