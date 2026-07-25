using Electric.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Electric.API.Database
{
    public class ElectricDbContext : DbContext
    {
        public ElectricDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<MeterEntity> Meters { get; set; }
        public DbSet<BillEntity> Bills { get; set; }
    }
}