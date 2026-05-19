using Microsoft.AspNetCore.Http;

namespace KASHOP.DAL.DTO.Request.Products
{
    public class ProductUpdateRequest
    {
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile>? SubImages { get; set; }
        public List<IFormFile>? NewImages { get; set; }
        public int? CategoryId { get; set; }
        public List<ProductTranslationRequest>? Translations { get; set; }
        public int? BrandId { get; set; }
    }
}
