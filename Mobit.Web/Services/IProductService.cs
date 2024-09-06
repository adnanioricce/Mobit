namespace Mobit.Services;
using Mobit.Models;

public interface IProductService 
{	
	Task<IEnumerable<ProductDto>> GetProductsAsync(Pagination pagination);
	Task<ProductDto?> GetProductByIdAsync(int productId);
	Task<ProductDto?> CreateProductAsync(ProductDto product);
	Task CreateProductsAsync(IEnumerable<Product> products);
	Task EditProductAsync(int productId,ProductDto product);
	Task DeleteProductAsync(int productId);
	
}
