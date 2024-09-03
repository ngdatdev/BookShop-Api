using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Payments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Request
/// </summary>
public class CreatePaymentLinkRequest : IFeatureRequest<CreatePaymentLinkResponse>
{
    public long OrderCode { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
    public List<ItemData> Items { get; set; }
    public string CancelUrl { get; set; }
    public string ReturnUrl { get; set; }

    public sealed class ItemData
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
