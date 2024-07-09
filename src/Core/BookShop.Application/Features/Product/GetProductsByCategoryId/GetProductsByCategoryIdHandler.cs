using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Handler
/// </summary>
public class GetProductsByCategoryIdHandler
    : IFeatureHandler<GetProductsByCategoryIdRequest, GetProductsByCategoryIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByCategoryIdHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetProductsByCategoryIdResponse> HandlerAsync(
        GetProductsByCategoryIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find category is not removed temporaliry.
        var category =
            await _unitOfWork.ProductFeature.GetProductsByCategoryIdRepository.FindCategoryByIdQueryAsync(
                categoryId: request.CategoryId,
                cancellationToken: cancellationToken
            );

        // Responds if catgoryId is not found
        if (Equals(objA: category, objB: default))
        {
            return new()
            {
                StatusCode = GetProductsByCategoryIdResponseStatusCode.CATEGORY_ID_IS_NOT_CORRECT
            };
        }

        // Find all products by categoryId
        var products =
            await _unitOfWork.ProductFeature.GetProductsByCategoryIdRepository.FindProductsByCategoryIdQueryAsync(
                categoryId: request.CategoryId,
                filterParameterQuery: new Data.Shared.FilterAndPagination.FilterParameterQuery()
                {
                    SortField = request.SortField,
                    Order = request.Order,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    MaxPrice = request.MaxPrice,
                    MinPrice = request.MinPrice,
                },
                cancellationToken: cancellationToken
            );

        // Get total number products.
        var countProduct =
            await _unitOfWork.ProductFeature.GetProductsByCategoryIdRepository.GetTotalNumberOfProductsByCategoryIdQueryAsync(
                categoryId: request.CategoryId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetProductsByCategoryIdResponse()
        {
            StatusCode = GetProductsByCategoryIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Products = new PaginationResponse<GetProductsByCategoryIdResponse.Body.Product>()
                {
                    Contents = products.Select(
                        product => new GetProductsByCategoryIdResponse.Body.Product()
                        {
                            FullName = product.FullName,
                            Author = product.Author,
                            ImageUrl = product.ImageUrl,
                            Discount = $"{product.Discount}%",
                            OriginalPrice = product.Price.ToString("0.000"),
                            SalePrice = (
                                product.Price * (1 - (product.Discount / 100.0m))
                            ).ToString("0.000"),
                        }
                    ),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countProduct / request.PageSize)
                },
                CategoryInfo = new()
                {
                    Name = category.FullName,
                    Description = category.Description,
                }
            }
        };
    }
}
