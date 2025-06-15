using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Services;
using EcommerceBackend.Models;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly IPaymentService _paymentService;

        public OrdersController(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;
             _paymentService = paymentService;
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
          var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                        return Unauthorized();
                        
            request.UserId = userId;
            var createdOrder = await _orderService.CreateOrderWithProducts(request);
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

       [HttpPut("update-method")]
        public async Task<IActionResult> UpdatePaymentMethod([FromQuery] int orderId, [FromQuery] string paymentMethod)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
                return NotFound("Order not found");

            var payment = order.Payments?.FirstOrDefault();
            if (payment == null)
                return NotFound("Payment not found for this order");

            payment.PaymentMethod = paymentMethod;
            payment.PaymentStatus = "PENDING";
            await _paymentService.UpdatePayment(payment);

            return NoContent();
        }

        private static readonly Dictionary<string, string> StatusFlow = new()
        {
            { "pending", "waiting payment" },
            { "waiting payment", "confirm" },
            { "confirm", "doing" },
            { "doing", "shipping" },
            { "shipping", "done" }
        };

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromQuery] int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
                return NotFound("Order not found");

            var currentStatus = order.OrderStatus?.ToLower();

            if (currentStatus == "pending")
            {
                var paymentMethod = order.Payments?.FirstOrDefault()?.PaymentMethod?.ToLower();
                if (paymentMethod == "cash")
                {
                    order.OrderStatus = "CONFIRM";
                    await _orderService.UpdateOrder(order);
                    return NoContent();
                }
            }

            if (!StatusFlow.TryGetValue(currentStatus, out var nextStatus))
                return BadRequest($"No next status for current status: {currentStatus}");

            order.OrderStatus = nextStatus.ToUpper();
            await _orderService.UpdateOrder(order);

            return NoContent();
        }

}