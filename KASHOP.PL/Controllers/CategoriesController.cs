using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using KASHOP.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository,IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _categoryRepository = categoryRepository;
        }
        [HttpGet("")]

        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(
            new{
                data= response,
                _localizer["Success"].Value
            });
        }

        [HttpPost("")]

        public IActionResult Create(CategoryRequest request) 
        {
            var category = request.Adapt<Category>();
            
            _categoryRepository.Create(category);
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

    } 
}
