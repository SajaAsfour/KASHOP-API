using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _categoryService = categoryService;
        }
        [HttpGet("")]

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(
            new{
                data= categories,
                _localizer["Success"].Value
            });
        }

        [HttpPost("")]

        public IActionResult Create(CategoryRequest request) 
        {
            var response = _categoryService.CreateCategory(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data= response
            });
        }

    } 
}
