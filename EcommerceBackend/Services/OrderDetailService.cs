using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderDetailService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
        {
            return await _unitOfWork.OrderDetails.GetAllAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _unitOfWork.OrderDetails.GetByIdAsync(id);
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
        {
            await _unitOfWork.OrderDetails.AddAsync(orderDetail);
            await _unitOfWork.CompleteAsync();
            return orderDetail;
        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            _unitOfWork.OrderDetails.Update(orderDetail);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteOrderDetail(int id)
        {
            var orderDetail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
            if (orderDetail != null)
            {
                _unitOfWork.OrderDetails.Remove(orderDetail);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}