using KASHOP.BLL.Service;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BrandsController(IBrandService brandService,IStringLocalizer<SharedResources> localizer)
        {
            _brandService = brandService;
            _localizer = localizer;
        }
    }
}
