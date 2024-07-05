using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.GetAllOrderDetailsByUserId;

/// <summary>
///     GetAllOrderDetailsByUserId Request
/// </summary>
public class GetAllOrderDetailsByUserIdRequest
    : IFeatureRequest<GetAllOrderDetailsByUserIdResponse> { }
