using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EcommerceBackend.Services;
using EcommerceBackend.Models;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviews();
            return Ok(reviews);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProduct(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProduct(productId);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var review = new Review
            {
                UserId = int.Parse(userId),
                ProductId = request.ProductId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            var createdReview = await _reviewService.CreateReview(review);
            return CreatedAtAction(nameof(GetReviewById), new { ReviewId = createdReview.ReviewId }, createdReview);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var existingReview = await _reviewService.GetReviewById(id);
            if (existingReview == null)
                return NotFound();

            if (existingReview.UserId != int.Parse(userId))
                return Forbid();

            existingReview.Rating = request.Rating;
            existingReview.Comment = request.Comment;

            await _reviewService.UpdateReview(existingReview);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var review = await _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound();

            if (review.UserId != int.Parse(userId))
                return Forbid();

            await _reviewService.DeleteReview(id);
            return NoContent();
        }
    }

    public class ReviewRequest
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}