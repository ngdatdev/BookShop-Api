using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///     RemoveUserPermanentlyById Response
/// </summary>
public class RemoveUserPermanentlyByIdResponse : IFeatureResponse
{
    public RemoveUserPermanentlyByIdResponseStatusCode StatusCode { get; init; }
}
