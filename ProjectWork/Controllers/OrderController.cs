using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models.Service;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OrdersController : ControllerBase
        {
            private readonly IOrderService _orderService;

            public OrdersController(IOrderService OrderService)
            {
                _orderService = OrderService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
            {
                var Orders = await _orderService.GetAllOrdersAsync();
                return Ok(Orders);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Order>> GetOrder(int id)
            {
                var Order = await _orderService.GetOrderByIdAsync(id);
                if (Order == null)
                    return NotFound();

                return Ok(Order);
            }

            [HttpPost]
            public async Task<ActionResult<Order>> PostOrder(Order Order)
            {
                var created = await _orderService.CreateOrderAsync(Order);
                return CreatedAtAction(nameof(GetOrder), new { id = created.OrderId }, created);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutOrder(int id, Order Order)
            {
                var updated = await _orderService.UpdateOrderAsync(id, Order);
                if (!updated)
                    return NotFound();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOrder(int id)
            {
                var deleted = await _orderService.DeleteOrderAsync(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }

            [HttpGet("hourly-average")]
            public async Task<IActionResult> GetAverageChecks()
            {
                var checks = await _orderService.GetAverageChecksAsync();
                return Ok(checks);
            }
        }
    }
}
