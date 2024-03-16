using Microsoft.AspNetCore.Mvc;
using TestProductWeb.DTOs;
using TestProductWeb.Services;

namespace TestProductWeb.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string searchBy, string searchValue)
        {
            var products = await _productService.RetrieveAllAsync();
            products = products.OrderByDescending(x => x.Id);

            if (products.Count() == 0)
            {
                TempData["InfoMessage"] = "Currently Products not available in the Database";
            }
            else
            {
                if (string.IsNullOrEmpty(searchValue))
                {
                    TempData["InfoMessage"] = "Please provide the search value.";
                    return View(products);
                }
                else
                {
                    if (products.Any())
                    {
                        if (searchBy.ToLower() == "productname")
                        {
                            var searchByProductName = products.Where(c => c.Name.ToLower().Contains(searchValue.ToLower()));
                            if (searchByProductName.Any())
                            {
                                return View(searchByProductName);
                            }
                            else
                            {
                                TempData["InfoMessage"] = "Currently Products not available in the Database.";
                            }
                        }
                        else if (searchBy.ToLower() == "productdescription")
                        {
                            var searchByProductName = products.Where(c => c.Description.ToLower().Contains(searchValue.ToLower()));
                            if (searchByProductName.Any())
                            {
                                return View(searchByProductName);
                            }
                            else
                            {
                                TempData["InfoMessage"] = "Currently Products not available in the Database.";
                            }
                        }
                    }
                }

            }
            return View(products);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                await _productService.CreateAsync(model);

                ModelState.Clear();
                TempData["SuccessMessage"] = "Furniture Created Successfully !";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ProductCreateDto.Name", "The Name is required");

                TempData["InfoMessage"] = "Please provide all the required fields";
                return View("Create");
            }
        }

        [HttpGet]
        public async Task<ViewResult> Edit(string id)
        {
            var product = await _productService.RetrieveByIdAsync(id);

            if (product is null)
                TempData["InfoMessage"] = "Product is not found !";

            ViewBag.Id = product.Id;
            ViewBag.Name = product.Name;
            ViewBag.Description = product.Description;

            ProductUpdateDto productUpdateDto = new ProductUpdateDto();
            return View("Edit", productUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductUpdateDto model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var result = await _productService.ModifyAsync(id, model);

                TempData["SuccessMessage"] = "Product Updated Successfully !";

                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ProductUpdateDto.Name", "The Name is required");

                TempData["InfoMessage"] = "Please provide all the required fields";
                return View("Edit", model);
            }
        }

        [HttpGet]
        public async Task<ViewResult> Delete(string id)
        {
            var product = await _productService.RetrieveByIdAsync(id);

            if (product is null)
                TempData["InfoMessage"] = "Product is not found !";

            return View("Delete", product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _productService.RemoveAsync(id);

            if (result != true)
                TempData["InfoMessage"] = "Product is not found !";
            
            return RedirectToAction("Index");
        
        }
    
    }
}
