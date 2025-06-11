using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using EcommerceBackend.Data;
using System.Security.Claims;

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
    public async Task<IActionResult> GetWishlist()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                 if (userId == null)
                     return Unauthorized();

                 // Parse userId safely
                 if (!int.TryParse(userId, out int userIdInt))
                     return BadRequest("Invalid user ID");
        var wishlist = await _context.WishlistItems
            .Where(w => w.UserId == userIdInt)
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
         var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             if (userId == null)
                 return Unauthorized();

             // Parse userId safely
             if (!int.TryParse(userId, out int userIdInt))
                 return BadRequest("Invalid user ID");

             bool exists = await _context.WishlistItems
                 .AnyAsync(w => w.UserId == userIdInt && w.ProductId == request.ProductId);

        if (exists)
            return BadRequest("Product already in wishlist.");

        var item = new WishlistItem
        {
            UserId = userIdInt,
            ProductId = request.ProductId
        };

        _context.WishlistItems.Add(item);
        await _context.SaveChangesAsync();

        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromWishlist(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        // Parse userId safely
        if (!int.TryParse(userId, out int userIdInt))
            return BadRequest("Invalid user ID");
        var item = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userIdInt);

        if (item == null) return NotFound();

        _context.WishlistItems.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Removed from wishlist" });
    }

    [HttpDelete]
    public async Task<IActionResult> ClearWishlist()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return Unauthorized();

                // Parse userId safely
                if (!int.TryParse(userId, out int userIdInt))
                    return BadRequest("Invalid user ID");

        var items = await _context.WishlistItems
            .Where(w => w.UserId == userIdInt)
            .ToListAsync();

        _context.WishlistItems.RemoveRange(items);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Wishlist cleared" });
    }
    public class WishlistAddRequest
    {
        public int ProductId { get; set; }
    }
}
