using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Models.Service.Interface;

namespace ProjectWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients([FromQuery]string filter, [FromQuery]int page, 
            [FromQuery]int page_size)
        {
            var clients = await _clientService.GetAllClientsAsync(filter, page, page_size);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            try
            {
                var created = await _clientService.CreateClientAsync(client);
                return CreatedAtAction(nameof(GetClient), new { id = created.ClientId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            var updated = await _clientService.UpdateClientAsync(id, client);
            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deleted = await _clientService.DeleteClientAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }

        [HttpGet("birthday-deliveries")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBirthdayCompleted()
        {
            var birthdays = await _clientService.GetBirthdayCompletedAsync();
            return Ok(birthdays);
        }
    }
}
