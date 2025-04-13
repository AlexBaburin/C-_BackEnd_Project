using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models.Service;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ClientsController : ControllerBase
        {
            private readonly IClientService _clientService;

            public ClientsController(IClientService clientService)
            {
                _clientService = clientService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Client>>> GetClients()
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Client>> GetClient(int id)
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }

            [HttpPost]
            public async Task<ActionResult<Client>> PostClient(Client client)
            {
                var created = await _clientService.CreateClientAsync(client);
                return CreatedAtAction(nameof(GetClient), new { id = created.ClientId }, created);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutClient(int id, Client client)
            {
                var updated = await _clientService.UpdateClientAsync(id, client);
                if (!updated)
                    return NotFound();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteClient(int id)
            {
                var deleted = await _clientService.DeleteClientAsync(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }

            [HttpGet("birthday-deliveries")]
            public async Task<IActionResult> GetBirthdayCompleted()
            {
                var birthdays = await _clientService.GetBirthdayCompletedAsync();
                return Ok(birthdays);
            }
        }
    }
}
