using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _unitOfWork.Payments.GetAllAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _unitOfWork.Payments.GetByIdAsync(id);
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            return payment;
        }

        public async Task UpdatePayment(Payment payment)
        {
            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment != null)
            {
                _unitOfWork.Payments.Remove(payment);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}