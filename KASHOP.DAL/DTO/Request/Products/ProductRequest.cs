using KASHOP.DAL.Validations;
using Microsoft.AspNetCore.Http;

namespace KASHOP.DAL.DTO.Request.Products
{
    public class ProductRequest
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        [AllowedExtensions(new string[] { ".png", ".jpg" })]
        [MaxFileSize(2)]
        public IFormFile MainImage { get; set; }
        public List<IFormFile> SubImages { get; set; }
        public int CategoryId { get; set; }
        public List<ProductTranslationRequest> Translations { get; set; }
        public int BrandId { get; set; }
    }
}
