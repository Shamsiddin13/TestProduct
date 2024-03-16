using Microsoft.AspNetCore.Mvc;
using TestProduct.DTOs;
using TestProduct.Services;

namespace TestProduct.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] ProductCreateDto dto)
        => Ok(await this._productService.CreateAsync(dto));

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await this._productService.RetrieveAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] string id)
            => Ok(await this._productService.RetrieveByIdAsync(id));

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByPropertyAsync([FromRoute(Name = "name")] string name)
            => Ok(await this._productService.SearchByProperty(name));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] string id)
            => Ok(await this._productService.RemoveAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] string id, [FromForm] ProductUpdateDto dto)
            => Ok(await this._productService.ModifyAsync(id, dto));


    }
}
