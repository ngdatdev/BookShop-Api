using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;

namespace BookShop.API.Controllers.Address.GetAllDistrictsByProvinceName.HttpResponseMapper;

/// <summary>
///     GetAllDistrictsByProvinceName http response
/// </summary>
internal sealed class GetAllDistrictsByProvinceNameHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllDistrictsByProvinceNameResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}