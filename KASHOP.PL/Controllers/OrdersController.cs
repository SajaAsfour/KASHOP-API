using KASHOP.BLL.Service;
using KASHOP.DAL.Models;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetUserOrderAsync(userId,id);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = order
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CancleOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.CancleOrderAsync(userId, id);
            if(!result) 
                return BadRequest();
            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }

        [HttpGet("admin")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderStatusEnum status = OrderStatusEnum.Pending)
        {
            var orders = await _orderService.GetAllOrdersAsync(status);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = orders
            });
        }
    }
}
