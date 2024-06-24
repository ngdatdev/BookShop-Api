using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.GetProductById;

/// <summary>
///     GetProductById Handler
/// </summary>
public class GetProductByIdHandler : IFeatureHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<GetProductByIdResponse> HandlerAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check product is temporarily removed.
        var isProductTemporarilyRemoved =
            await _unitOfWork.ProductFeature.GetProductByIdRepository.IsTemporarilyRemovedProductQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is temporarily removed.
        if (isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetProductByIdResponseStatusCode.PRODUCT_IS_TEMPORARILY_REMOVED
            };
        }

        var product =
            await _unitOfWork.ProductFeature.GetProductByIdRepository.FindProductByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetProductByIdResponse()
        {
            StatusCode = GetProductByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                ProductInfo = new GetProductByIdResponse.Body.Product()
                {
                    FullName = product.FullName,
                    Author = product.Author,
                    ImageUrl = product.ImageUrl,
                    Discount = $"{product.Discount}%",
                    OriginalPrice = product.Price.ToString("0.000"),
                    SalePrice = (product.Price * (1 - (product.Discount / 100.0m))).ToString(
                        "0.000"
                    ),
                    Languages = product.Languages,
                    NumberOfPage = product.NumberOfPage,
                    Publisher = product.Publisher,
                    QuantityCurrent = product.QuantityCurrent,
                    QuantitySold = product.QuantitySold,
                    SubUrls = product.Assets.Select(asset => asset.ImageUrl).ToList(),
                }
            }
        };
    }
}
