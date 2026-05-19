using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Request.Brands
{
    public class BrandUpdateRequest
    {
        public IFormFile? Logo { get; set; }
        public List<BrandTranslationRequest>? BrandTranslations { get; set; }
    }
}
