using Microsoft.EntityFrameworkCore;
using CheckIn.Models;

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
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<WorkArea> WorkArea { get; set; }
    }
}
