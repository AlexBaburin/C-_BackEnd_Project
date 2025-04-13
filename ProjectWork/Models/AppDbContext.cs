using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWork.Models
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BirthdayOrder>()
           .ToTable("BirthdayOrder", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<AverageCheck>()
           .ToTable("AverageCheck", t => t.ExcludeFromMigrations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BirthdayOrder> BirthdayOrders { get; set; }
        public async Task<List<BirthdayOrder>> GetBirthdayCompleted()
        {
            return await this.BirthdayOrders
                                   .FromSqlRaw("SELECT * FROM birthdaycompleted()")
                                   .ToListAsync();
        }
        public DbSet<AverageCheck> AverageChecks { get; set; }
        public async Task<List<AverageCheck>> GetAverageChecks()
        {
            return await this.AverageChecks
                                  .FromSqlRaw("SELECT * FROM averagecheck()")
                                  .ToListAsync();
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> Statuses { get; set; }
    }
}
