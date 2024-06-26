using System;

namespace BookShop.Application.Shared.Common;

/// <summary>
///     Represent default guid constant.
/// </summary>
public static class CommonConstant
{
    public static readonly Guid DEFAULT_ENTITY_ID_AS_GUID = Guid.Parse(
        "c8500b46-b134-4b60-85b7-8e6af1187e1c"
    );
    public static readonly DateTime MIN_DATE_TIME = DateTime.MinValue.ToUniversalTime();
}
    