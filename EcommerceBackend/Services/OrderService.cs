using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _unitOfWork.Orders.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }

        public async Task UpdateOrder(Order order)
        {
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order != null)
            {
                _unitOfWork.Orders.Remove(order);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<Order> CreateOrderWithProducts(List<Product> products,string userId)
        {
            var order = new Order
            {
                UserId = int.Parse(userId),
                TotalAmount = products.Sum(p => p.Price),
                OrderDetails = products.Select(p => new OrderDetail
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity, 
                    Price = p.Price,
                    CreatedAt = DateTime.UtcNow,
                }).ToList(),
                OrderStatus = "PENDING",
                CreatedAt = DateTime.UtcNow,
    
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }
    }
}