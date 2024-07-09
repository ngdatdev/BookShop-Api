using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;

namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.HttpResponseMapper;

/// <summary>
///     GetAllUsersTemporarilyRemovedById http response
/// </summary>
internal sealed class GetAllUsersTemporarilyRemovedByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllUsersTemporarilyRemovedByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllUsersTemporarilyRemovedByIdResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
