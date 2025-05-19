using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EcommerceBackend.Services;
using EcommerceBackend.Models;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cart")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemsService _cartItemsService;

        public CartItemController(ICartItemsService cartItemsService)
        {
            _cartItemsService = cartItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var cartItems = await _cartItemsService.GetCartItemsByUserId(userId);
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var cartItem = await _cartItemsService.GetCartItemById(id);
            if (cartItem == null)
                return NotFound();

            // Verify the cart item belongs to the current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != cartItem.UserId.ToString())
                return Forbid();

            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var cartItem = new CartItem
            {
                UserId = int.Parse(userId),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _cartItemsService.AddToCart(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { CartItemId = result.CartItemId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartItemRequest request)
        {
            var existingItem = await _cartItemsService.GetCartItemById(id);
            if (existingItem == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != existingItem.UserId.ToString())
                return Forbid();

            existingItem.Quantity = request.Quantity;
            await _cartItemsService.UpdateCartItem(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _cartItemsService.GetCartItemById(id);
            if (cartItem == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != cartItem.UserId.ToString())
                return Forbid();

            await _cartItemsService.DeleteCartItem(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            await _cartItemsService.ClearCart(userId);
            return NoContent();
        }
    }

    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
    }
}