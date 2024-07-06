using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAddressesByWard;

/// <summary>
///     GetAddressesByWard Request
/// </summary>
public class GetAddressesByWardRequest : IFeatureRequest<GetAddressesByWardResponse>
{
    public string Ward { get; set; }
}
