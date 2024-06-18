using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Handler
/// </summary>
public class CreateProductHandler : IFeatureHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IUnitOfWork unitOfWork)
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
    public async Task<CreateProductResponse> HandlerAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        // Response successfully.
        return new CreateProductResponse()
        {
            StatusCode = CreateProductResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { }
        };
    }
}
