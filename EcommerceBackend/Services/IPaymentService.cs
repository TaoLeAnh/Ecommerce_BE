using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<Payment> CreatePayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task DeletePayment(int id);
    }
}