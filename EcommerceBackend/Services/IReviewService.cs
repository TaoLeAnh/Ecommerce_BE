using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task<Review> CreateReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(int id);
    }
}