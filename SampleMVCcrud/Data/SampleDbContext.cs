using Microsoft.EntityFrameworkCore;
using SampleMVCcrud.Models.Domain;

namespace SampleMVCcrud.Data
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
