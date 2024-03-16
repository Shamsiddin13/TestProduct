using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestProduct.DbContexts;
using TestProduct.IRepositories;
using TestProduct.Models;

namespace TestProduct.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly TestDbContext _dbContext;
        protected readonly DbSet<Product> _dbSet;

        public ProductRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Product>();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var product = await _dbSet.FirstOrDefaultAsync(p => p.Id.ToString() == id);
            _dbSet.Remove(product);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            var entry = await _dbSet.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public IQueryable<Product> Search(Expression<Func<Product, bool>> predicate)
        {
            return _dbContext.Product.Where(predicate);
        }

        public IQueryable<Product> SelectAll()
            => _dbSet;


        public async Task<Product> SelectByIdAsync(string id)
            => await _dbSet.FirstOrDefaultAsync(p => p.Id.ToString() == id);

        public async Task<Product> UpdateAsync(Product product)
        {
            var entry = _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
