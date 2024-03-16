using TestProductWeb.IRepositories;
using TestProductWeb.Repositories;
using TestProductWeb.Services;

namespace TestProductWeb.Extensions
{
	public static class ServiceExtension
	{
		public static void AddCustomService(this IServiceCollection services)
		{
			// Product
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductService, ProductService>();
		}
	}
}
