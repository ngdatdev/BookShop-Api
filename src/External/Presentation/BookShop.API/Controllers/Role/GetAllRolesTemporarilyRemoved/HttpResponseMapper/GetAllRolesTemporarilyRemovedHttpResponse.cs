using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Roles.GetAllRolesTemporarilyRemoved;

namespace BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved.HttpResponseMapper;

/// <summary>
///     GetAllRolesTemporarilyRemoved http response
/// </summary>
internal sealed class GetAllRolesTemporarilyRemovedHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllRolesTemporarilyRemovedResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
