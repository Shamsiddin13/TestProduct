using System.Linq.Expressions;
using TestProductWeb.Models;

namespace TestProductWeb.IRepositories
{
    public interface IProductRepository
    {
        IQueryable<Product> SelectAll();
        Task<bool> DeleteAsync(string id);
        Task<Product> SelectByIdAsync(string id);
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        IQueryable<Product> Search(Expression<Func<Product, bool>> predicate);
    }
}
