using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.Products.Remove(product);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}