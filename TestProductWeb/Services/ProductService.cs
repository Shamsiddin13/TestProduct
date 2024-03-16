using Microsoft.EntityFrameworkCore;
using TestProductWeb.DTOs;
using TestProductWeb.Exceptions;
using TestProductWeb.IRepositories;
using TestProductWeb.Models;

namespace TestProductWeb.Services
{
	public class ProductService : IProductService
	{
		protected readonly IProductRepository _repository;

		public ProductService(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<ProductViewDto> CreateAsync(ProductCreateDto dto)
		{
			var product = await _repository.SelectAll()
										   .Where(p => p.Name.ToLower() == dto.Name.ToLower())
										   .AsNoTracking()
										   .FirstOrDefaultAsync();

			if (product is not null)
				throw new ProductException(409, "Product is already exist !");

			var newProduct = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
			};

			var result = await _repository.InsertAsync(newProduct);

			// В этом случае я мог бы использовать Mapper. 
			// Я не использовал пакет Mapper из-за небольшого количества свойств модели продукта.

			var viewDto = new ProductViewDto
			{
				Id = result.Id.ToString(),
				Name = result.Name,
				Description = result.Description,
			};

			return viewDto;
		}

		public async Task<ProductViewDto> ModifyAsync(string id, ProductUpdateDto dto)
		{
			var product = await _repository.SelectAll()
										   .Where(p => p.Id.ToString() == id)
										   .AsNoTracking()
										   .FirstOrDefaultAsync();

			if (product is null)
				throw new ProductException(404, "Product is not found !");

			var newProduct = new Product
			{
				Id = Guid.Parse(id),
				Name = dto.Name,
				Description = dto.Description,
			};

			await _repository.UpdateAsync(newProduct);

			var viewDto = new ProductViewDto
			{
				Id = newProduct.Id.ToString(),
				Name = newProduct.Name,
				Description = newProduct.Description,
			};

			return viewDto;
		}

		public async Task<bool> RemoveAsync(string id)
		{
			var product = await _repository.SelectAll()
										   .Where(p => p.Id.ToString() == id)
										   .AsNoTracking()
										   .FirstOrDefaultAsync();

			if (product is null)
				throw new ProductException(404, "Product is not found !");

			return await _repository.DeleteAsync(id);
		}

		public async Task<IEnumerable<ProductViewDto>> RetrieveAllAsync()
		{
			var products = await _repository.SelectAll()
											.AsNoTracking()
											.ToListAsync();

			var result = products.Select(p => new ProductViewDto
			{
				Id = p.Id.ToString(),
				Name = p.Name,
				Description = p.Description,
			});

			return result;
		}

		public async Task<ProductViewDto> RetrieveByIdAsync(string id)
		{
			var product = await _repository.SelectAll()
										   .Where(p => p.Id.ToString() == id)
										   .AsNoTracking()
										   .FirstOrDefaultAsync();

			if (product is null)
				throw new ProductException(404, "Product is not found !");

			var result = new ProductViewDto
			{
				Id = product.Id.ToString(),
				Name = product.Name,
				Description = product.Description,
			};

			return result;
		}

		public async Task<ProductViewDto> SearchByProperty(string searchTerm)
		{
			var product = await _repository.Search(p => p.Name.Contains(searchTerm)).FirstOrDefaultAsync();

			var result = new ProductViewDto
			{
				Id = product.Id.ToString(),
				Name = product.Name,
				Description = product.Description,
			};

			return result;
		}
	}
}
