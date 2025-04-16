using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models.Service.Interface;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService StatusService)
        {
            _statusService = StatusService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetStatuses()
        {
            var Statuss = await _statusService.GetAllStatusesAsync();
            return Ok(Statuss);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderStatus>> GetStatus(int id)
        {
            var Status = await _statusService.GetStatusByIdAsync(id);
            if (Status == null)
                return NotFound();

            return Ok(Status);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderStatus>> PostStatus(OrderStatus Status)
        {
            try
            {
                var created = await _statusService.CreateStatusAsync(Status);
                return CreatedAtAction(nameof(GetStatus), new { id = created.StatusId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutStatus(int id, OrderStatus Status)
        {
            var updated = await _statusService.UpdateStatusAsync(id, Status);
            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var deleted = await _statusService.DeleteStatusAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}

