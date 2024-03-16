using TestProduct.IRepositories;
using TestProduct.Repositories;
using TestProduct.Services;

namespace TestProduct.Extensions
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
