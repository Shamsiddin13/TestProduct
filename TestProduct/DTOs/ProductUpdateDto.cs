using System.ComponentModel.DataAnnotations;

namespace TestProduct.DTOs
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Enter a product name"), StringLength(255, ErrorMessage = "The name of maximum length is 255")]
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
