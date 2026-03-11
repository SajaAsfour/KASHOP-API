using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(
            new{
                data= categories,
                _localizer["Success"].Value
            });
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create(CategoryRequest request) 
        {
            var response = await _categoryService.CreateCategory(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data= response
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetCategory(c => c.Id == id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return NotFound(new
                {
                    message = _localizer["NotFound"].Value
                });
            }

            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

    } 
}
