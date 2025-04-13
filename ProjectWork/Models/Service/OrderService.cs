using Microsoft.EntityFrameworkCore;

namespace ProjectWork.Models.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AverageCheck>> GetAverageChecksAsync()
        {
            return await _context.GetAverageChecks();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order Order)
        {
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<bool> UpdateOrderAsync(int id, Order Order)
        {
            if (id != Order.OrderId)
                return false;

            _context.Entry(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Orders.AnyAsync(c => c.OrderId == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var Order = await _context.Orders.FindAsync(id);
            if (Order == null)
                return false;

            _context.Orders.Remove(Order);
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
