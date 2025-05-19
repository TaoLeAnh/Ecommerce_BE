using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Services;
using EcommerceBackend.Models;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPost("with-products")]
        public async Task<IActionResult> CreateOrderWithProducts([FromBody] CreateOrderWithProductsRequest request)
        {
            var createdOrder = await _orderService.CreateOrderWithProducts(request.Products, request.UserId);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderId)
                return BadRequest("Order ID mismatch");

            await _orderService.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return NoContent();
        }
    }

    // Request model for CreateOrderWithProducts
    public class CreateOrderWithProductsRequest
    {
        public List<Product> Products { get; set; }
        public string UserId { get; set; }
    }
}