using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Payments.GetPaymentsByMethod;

/// <summary>
///     GetPaymentsByMethod Request
/// </summary>
public class GetPaymentsByMethodRequest : IFeatureRequest<GetPaymentsByMethodResponse>
{
    [FromRoute]
    public string Method { get; init; }

    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
