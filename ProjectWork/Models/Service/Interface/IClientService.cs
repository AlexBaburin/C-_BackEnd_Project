namespace ProjectWork.Models.Service.Interface
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<bool> UpdateClientAsync(int id, Client client);
        Task<bool> DeleteClientAsync(int id);
        Task<List<BirthdayOrder>> GetBirthdayCompletedAsync();
    }
}
