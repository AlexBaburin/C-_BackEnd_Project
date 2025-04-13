namespace ProjectWork.Models.Service.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order Order);
        Task<bool> UpdateOrderAsync(int id, Order Order);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<Check>> GetAverageChecksAsync();
    }
}
