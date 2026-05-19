using Microsoft.AspNetCore.Http;

namespace KASHOP.DAL.DTO.Request.Brands
{
    public class BrandRequest
    {
        public IFormFile Logo { get; set; }
        public List<BrandTranslationRequest> BrandTranslations { get; set; }
    }
}
