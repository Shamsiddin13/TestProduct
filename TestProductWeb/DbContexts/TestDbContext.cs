using Microsoft.EntityFrameworkCore;
using TestProductWeb.Models;

namespace TestProductWeb.DbContexts
{
	public partial class TestDbContext : DbContext
	{
		public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Product> Product { get; set; }
	}
}
