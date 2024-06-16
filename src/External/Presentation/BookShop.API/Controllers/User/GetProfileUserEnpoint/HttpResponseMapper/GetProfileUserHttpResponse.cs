using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Users.GetProfileUser;

namespace BookShop.API.Controllers.User.GetProfileUserEndpoint.HttpResponseMapper;

/// <summary>
///     GetProfileUser http response
/// </summary>
internal sealed class GetProfileUserHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetProfileUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
