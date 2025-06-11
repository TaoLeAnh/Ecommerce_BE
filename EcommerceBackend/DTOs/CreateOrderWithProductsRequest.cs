// Request model for CreateOrderWithProducts
using EcommerceBackend.Models;
public class CreateOrderWithProductsRequest
{
    public List<Product> Products { get; set; }
    public string? UserId { get; set; }
    public string Address { get; set; }
    public string Note { get; set; }
    public decimal ShipCost { get; set; }
    public decimal GrandTotal { get; set; }
}