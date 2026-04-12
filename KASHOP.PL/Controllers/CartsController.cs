using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartSerivce _cartSerivce;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CartsController(ICartSerivce cartSerivce , IStringLocalizer<SharedResources> localizer)
        {
            _cartSerivce = cartSerivce;
            _localizer = localizer;
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartSerivce.AddToCartAsync(request, UserId);

            if (!result) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }
    }
}
