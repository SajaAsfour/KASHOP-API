using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CategoriesController(ApplicationDbContext context,IStringLocalizer<SharedResources> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        [HttpGet("")]

        public IActionResult Get()
        {
            var categories = _context.Categories.Include( c => c.Translations).ToList();
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
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

    } 
}
