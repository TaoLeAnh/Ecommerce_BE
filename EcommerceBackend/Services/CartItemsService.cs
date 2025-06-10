using EcommerceBackend.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;

namespace EcommerceBackend.Services
{
    public class CartItemsService : ICartItemsService
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly IProductService _productService;

        public CartItemsService(UnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(string userId)
        {

            IEnumerable<CartItem> cartItems = await _unitOfWork.CarItems.FindAsync(c => c.UserId == int.Parse(userId));
            foreach (CartItem cartItem in cartItems)
            {
                Product product = await _productService.GetProductById(cartItem.ProductId);
                cartItem.Product = product;
            }
            return cartItems;
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            return await _unitOfWork.CarItems.GetByIdAsync(id);
        }

        public async Task<CartItem> AddToCart(CartItem cartItem)
        {
            if (cartItem == null)
                throw new ArgumentNullException(nameof(cartItem), "CartItem cannot be null!");

            // Có thể cần check thêm:
            if (cartItem.UserId == 0)
                throw new ArgumentException("UserId is required and must be > 0!");
            if (cartItem.ProductId == 0)
                throw new ArgumentException("ProductId is required and must be > 0!");

            var existingItem = await _unitOfWork.CarItems.FindAsync(
                c => c.UserId == cartItem.UserId && c.ProductId == cartItem.ProductId
            ) ?? new List<CartItem>();

            var item = existingItem.FirstOrDefault();

            if (item != null)
            {
                item.Quantity += cartItem.Quantity;
                _unitOfWork.CarItems.Update(item);
            }
            else
            {
                await _unitOfWork.CarItems.AddAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync();
            return item ?? cartItem;
        }


        public async Task UpdateCartItem(CartItem cartItem)
        {
            _unitOfWork.CarItems.Update(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCartItem(int id)
        {
            var cartItem = await _unitOfWork.CarItems.GetByIdAsync(id);
            if (cartItem != null)
            {
                _unitOfWork.CarItems.Remove(cartItem);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task ClearCart(string userId)
        {
            var allItems = await _unitOfWork.CarItems
                .GetAllAsync();
            var cartItems = allItems.Where(c => c.UserId == int.Parse(userId));     
            

            foreach (var item in cartItems)
            {
                _unitOfWork.CarItems.Remove(item);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}