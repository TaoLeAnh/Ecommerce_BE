using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UnitOfWork _unitOfWork;

        public ReviewService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _unitOfWork.Reviews.GetAllAsync();
        }

        public async Task<Review> GetReviewById(int id)
        {
            return await _unitOfWork.Reviews.GetByIdAsync(id);
        }

        public async Task<Review> CreateReview(Review review)
        {
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.CompleteAsync();
            return review;
        }

        public async Task UpdateReview(Review review)
        {
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteReview(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review != null)
            {
                _unitOfWork.Reviews.Remove(review);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}