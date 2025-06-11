using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task<Order> CreateOrderWithProducts(CreateOrderWithProductsRequest request);
    }
}