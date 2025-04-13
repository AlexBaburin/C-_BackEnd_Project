using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Models.Service.Interface;

namespace ProjectWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService OrderService)
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
        public async Task<ActionResult<Order>> PostOrder(OrderViewModel OrderVM)
        {
            var Order = new Order
            {
                OrderId = OrderVM.OrderId,
                Amount = OrderVM.Amount,
                ClientId = OrderVM.ClientId,
                StatusId = OrderVM.StatusId,
                Time = OrderVM.Time
            };
            try
            {
                var created = await _orderService.CreateOrderAsync(Order);
                return CreatedAtAction(nameof(GetOrder), new { id = created.OrderId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderViewModel OrderVM)
        {
            var Order = new Order
            {
                OrderId = OrderVM.OrderId,
                Amount = OrderVM.Amount,
                ClientId = OrderVM.ClientId,
                StatusId = OrderVM.StatusId,
                Time = OrderVM.Time
            };
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

