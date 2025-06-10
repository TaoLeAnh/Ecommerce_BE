using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<PagedResult<Product>> FilterProducts(ProductFilterDto filter);
    }
}