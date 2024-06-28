using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///     RemoveUserTemporarilyById Response
/// </summary>
public class RemoveUserTemporarilyByIdResponse : IFeatureResponse
{
    public RemoveUserTemporarilyByIdResponseStatusCode StatusCode { get; init; }
}
