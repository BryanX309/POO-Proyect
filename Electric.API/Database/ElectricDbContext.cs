using Microsoft.EntityFrameworkCore;

namespace Electric.API.Database
{
    public class ElectricDbContext : DbContext
    {
        public ElectricDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}