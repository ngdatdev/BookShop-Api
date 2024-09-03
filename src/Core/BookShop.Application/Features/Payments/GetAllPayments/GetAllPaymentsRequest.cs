using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Payments.GetAllPayments;

/// <summary>
///     GetAllPayments Request
/// </summary>
public class GetAllPaymentsRequest : IFeatureRequest<GetAllPaymentsResponse>
{
    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
