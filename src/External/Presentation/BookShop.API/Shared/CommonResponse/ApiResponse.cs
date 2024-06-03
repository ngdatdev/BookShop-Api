using System;
using System.Collections.Generic;
using BookShop.API.AppCodes;

namespace BookShop.API.CommonResponse;

/// <summary>
///     Contain common response for all api.
/// </summary>
/// <remarks>
///     All http responses format must be this format.
/// </remarks>
internal sealed class ApiResponse
{
    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public int AppCode { get; init; } = (int)CommonAppCode.SUCCESS;
    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
