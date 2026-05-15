using KASHOP.BLL.Service;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrdersController(IOrderService orderService ,IStringLocalizer<SharedResources> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = orders
            });
        }
    }
}
