using Microsoft.EntityFrameworkCore;
using TestProduct.Models;

namespace TestProduct.DbContexts
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
