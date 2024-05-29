using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookShop.API.HttpResponseMapper.ErrorApiResponse;

/// <summary>
///     Represents a error HTTP response with a status code, error messages.
/// </summary>
public class ErrorHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; init; }

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
