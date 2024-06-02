using Microsoft.AspNetCore.Authorization;

namespace BookShop.API.Policy.Authorization;

/// <summary>
///     Represents a custom authorization requirement.
/// </summary>
public sealed class ValidationUserRequirement : IAuthorizationRequirement { }
