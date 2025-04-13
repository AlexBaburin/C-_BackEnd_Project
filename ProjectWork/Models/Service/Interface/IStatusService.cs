namespace ProjectWork.Models.Service.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<OrderStatus>> GetAllStatusesAsync();
        Task<OrderStatus?> GetStatusByIdAsync(int id);
        Task<OrderStatus> CreateStatusAsync(OrderStatus Status);
        Task<bool> UpdateStatusAsync(int id, OrderStatus Status);
        Task<bool> DeleteStatusAsync(int id);
    }
}
