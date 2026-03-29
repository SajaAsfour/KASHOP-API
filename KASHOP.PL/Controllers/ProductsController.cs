using Azure;
using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ProductsController(IProductService productService,
            IStringLocalizer<SharedResources> localizer) 
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new
            {
                data = products,
                message = _localizer["Success"].Value
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(new
            {
                data = product,
                message = _localizer["Success"].Value
            });
        }

        [HttpPost("")]
        [Authorize]

        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            await _productService.CreateProductAsync(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }
    }
}
