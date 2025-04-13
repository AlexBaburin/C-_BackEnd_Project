using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWork.Models.Service.Interface;

namespace ProjectWork.Models.Service
{
    public class StatusService : IStatusService
    {
        private readonly AppDbContext _context;

        public StatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<OrderStatus?> GetStatusByIdAsync(int id)
        {
            return await _context.Statuses.FindAsync(id);
        }

        public async Task<OrderStatus> CreateStatusAsync(OrderStatus Status)
        {
            _context.Statuses.Add(Status);
            await _context.SaveChangesAsync();
            return Status;
        }

        public async Task<bool> UpdateStatusAsync(int id, OrderStatus Status)
        {
            if (id != Status.StatusId)
                return false;

            _context.Entry(Status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Statuses.AnyAsync(c => c.StatusId == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteStatusAsync(int id)
        {
            var Status = await _context.Statuses.FindAsync(id);
            if (Status == null)
                return false;

            _context.Statuses.Remove(Status);
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
