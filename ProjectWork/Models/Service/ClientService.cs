using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Models.Service.Interface;

namespace ProjectWork.Models.Service
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BirthdayOrder>> GetBirthdayCompletedAsync()
        {
            return await _context.GetBirthdayCompleted();
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync(string filter, int page, int page_size)
        {
            var clients = await _context.Clients.ToListAsync();
            List<Client> result = new List<Client>();
            if (!String.IsNullOrWhiteSpace(filter))
            {
                foreach (var client in clients)
                {
                    if (client.ClientId.ToString() == filter ||
                        client.Surname == filter ||
                        client.Name == filter ||
                        client.BirthDate.ToString() == filter)
                    {
                        result.Add(client);
                    }
                }
            }
            if (page_size < result.Count)
            {
                clients.Clear();
                for (int i = (page - 1) * page_size; i < result.Count || i < page * page_size; i++)
                {
                    clients.Add(result[i]);
                }
                result.Clear();
                result = clients;
            }
            return result;
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> UpdateClientAsync(int id, Client client)
        {
            if (id != client.ClientId)
                return false;

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Clients.AnyAsync(c => c.ClientId == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;

            _context.Clients.Remove(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
