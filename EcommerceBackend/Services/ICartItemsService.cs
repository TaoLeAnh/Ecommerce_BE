using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface ICartItemsService
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserId(string userId);
        Task<CartItem> GetCartItemById(int id);
        Task<CartItem> AddToCart(CartItem cartItem);
        Task UpdateCartItem(CartItem cartItem);
        Task DeleteCartItem(int id);
        Task ClearCart(string userId);
    }
}