using KASHOP.BLL.Service.Brand;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(new
            {
                data = brands,
                message = _localizer["Success"].Value

            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var brand = await _brandService.GetBrandAsync(b => b.Id == id);
            if (brand == null) return NotFound();
            return Ok(new
            {
                data = brand,
                message = _localizer["Success"].Value

            });
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BrandRequest request)
        {
            await _brandService.CreateBrandAsync(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _brandService.DeleteBrandAsync(id);
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

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id , [FromForm] BrandUpdateRequest request)
        {
            var updated = await _brandService.UpdateBrandAsync(id, request);
            if (!updated) return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

        [HttpPatch("{id}/status")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var changed = await _brandService.ToggleStatusAsync(id);
            if(!changed) return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }
    }
}
