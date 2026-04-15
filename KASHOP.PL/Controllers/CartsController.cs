using KASHOP.BLL.Service;
using KASHOP.DAL;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Request;
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
    [Authorize]

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

        [HttpGet("")]
        public async Task<IActionResult> GetCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _cartSerivce.GetCartAsync(UserId);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = items
            });
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] int productId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var removed = await _cartSerivce.RemoveItemAsync(productId , UserId);

            if(!removed) return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var removed = await _cartSerivce.ClearCartAsync(UserId);

            if (!removed) return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute]int productId,
            [FromBody] UpdateCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var updated = await _cartSerivce.UpdateQuantityAsync(productId, request.Count , UserId);

            if(!updated) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value
            });

        }
    }
}
