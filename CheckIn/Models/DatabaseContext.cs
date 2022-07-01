using Microsoft.EntityFrameworkCore;

namespace CheckIn.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        //Models
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Registration> Records { get; set; }

        
    }
}
