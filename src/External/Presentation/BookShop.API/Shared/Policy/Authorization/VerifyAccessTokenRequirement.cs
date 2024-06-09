using Microsoft.AspNetCore.Authorization;

namespace BookShop.API.Shared.Policy.Authorization;

/// <summary>
///     Represents a custom authorization requirement.
/// </summary>
public sealed class VerifyAccessTokenRequirement : IAuthorizationRequirement { }
