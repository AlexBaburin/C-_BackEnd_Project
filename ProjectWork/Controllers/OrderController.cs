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
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] string filter, [FromQuery] int page,
            [FromQuery] int page_size)
        {
            var Orders = await _orderService.GetAllOrdersAsync(filter, page, page_size);
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var Order = await _orderService.GetOrderByIdAsync(id);
            if (Order == null)
                return NotFound();

            return Ok(Order);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }

        [HttpGet("hourly-average")]
        public async Task<IActionResult> GetAverageChecks()
        {
            var checks = await _orderService.GetAverageChecksAsync();
            return Ok(checks);
        }
    }
}

