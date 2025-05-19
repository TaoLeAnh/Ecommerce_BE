using EcommerceBackend.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;

namespace EcommerceBackend.Services
{
    public class CartItemsService : ICartItemsService
    {
        private readonly UnitOfWork _unitOfWork;

        public CartItemsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(string userId)
        {
            return await _unitOfWork.CarItems.FindAsync(c => c.UserId == int.Parse(userId));
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            return await _unitOfWork.CarItems.GetByIdAsync(id);
        }

        public async Task<CartItem> AddToCart(CartItem cartItem)
        {
            var existingItem = await _unitOfWork.CarItems.FindAsync(c => c.UserId == cartItem.UserId && c.ProductId == cartItem.ProductId) ;
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