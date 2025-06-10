using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using EcommerceBackend.Data;

[Route("api/wishlist")]
[ApiController]
public class WishlistItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WishlistItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlist([FromQuery] int userId)
    {
        var wishlist = await _context.WishlistItems
            .Where(w => w.UserId == userId)
            .Include(w => w.Product)
            .ToListAsync();

        return Ok(wishlist.Select(w => new
        {
            wishlistItemId = w.Id,
            product = w.Product
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist([FromBody] WishlistAddRequest request)
    {
        bool exists = await _context.WishlistItems
            .AnyAsync(w => w.UserId == request.UserId && w.ProductId == request.ProductId);

        if (exists)
            return BadRequest("Product already in wishlist.");

        var item = new WishlistItem
        {
            UserId = request.UserId,
            ProductId = request.ProductId
        };

        _context.WishlistItems.Add(item);
        await _context.SaveChangesAsync();

        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromWishlist(int id, [FromQuery] int userId)
    {
        var item = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

        if (item == null) return NotFound();

        _context.WishlistItems.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Removed from wishlist" });
    }

    [HttpDelete]
    public async Task<IActionResult> ClearWishlist([FromQuery] int userId)
    {
        var items = await _context.WishlistItems
            .Where(w => w.UserId == userId)
            .ToListAsync();

        _context.WishlistItems.RemoveRange(items);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Wishlist cleared" });
    }
    public class WishlistAddRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
