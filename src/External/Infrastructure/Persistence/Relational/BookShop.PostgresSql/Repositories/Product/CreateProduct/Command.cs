using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.PostgresSql.Repositories.Product.CreateProduct;

/// <summary>
///    Implement of command ICreateProduct Repository.
/// </summary>
internal partial class CreateProductRepository
{
    public async Task<bool> CreateProductCommandAsync(
        BookShop.Data.Shared.Entities.Product product,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _products.Add(entity: product);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            return false;
        }
        return true;
    }
}
