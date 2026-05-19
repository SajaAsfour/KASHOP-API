using KASHOP.BLL.Service.Checkouts;
using KASHOP.DAL.DTO.Request.Checkouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutsController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutsController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody] CheckoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkoutService.ProcessCheckout(userId, request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromQuery] string sessionId)
        {
            var result = await _checkoutService.HandleSuccess(sessionId);
            return Ok(new { message = "success" });
        }
    }
}
