using System;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Payments.UpdatePaymentByWebHook;

/// <summary>
///     UpdatePaymentByWebHook Response
/// </summary>
public class UpdatePaymentByWebHookResponse : IFeatureResponse
{
    public UpdatePaymentByWebHookResponseStatusCode StatusCode { get; init; }
}
