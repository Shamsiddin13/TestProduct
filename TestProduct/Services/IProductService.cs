using System.Linq.Expressions;
using TestProduct.DTOs;
using TestProduct.Models;

namespace TestProduct.Services
{
    public interface IProductService
    {
        Task<bool> RemoveAsync(string id);
        Task<ProductViewDto> RetrieveByIdAsync(string id);
        Task<IEnumerable<ProductViewDto>> RetrieveAllAsync();
        Task<ProductViewDto> CreateAsync(ProductCreateDto dto);
        Task<ProductViewDto> SearchByProperty(string searchTerm);
        Task<ProductViewDto> ModifyAsync(string id, ProductUpdateDto dto);
    }
}
