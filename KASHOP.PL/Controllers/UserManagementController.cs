using KASHOP.BLL.Service.UserManagements;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementSerivce _userManagementSerivce;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UserManagementController(IUserManagementSerivce userManagementSerivce , IStringLocalizer<SharedResources> localizer)
        {
            _userManagementSerivce = userManagementSerivce;
            _localizer = localizer;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManagementSerivce.GetAllUsersAsync();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = users
            });
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManagementSerivce.GetUserAsync(userId);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = user
            });
        }

        [HttpPatch("users/{userId}/role")]
        public async Task<IActionResult> ChangeRole(string userId , [FromBody] ChangeRoleRequest request)
        {
            var result = await _userManagementSerivce.ChangeRoleAsync(userId ,request.newRole);

            if(!result) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }

        [HttpPatch("users/{userId}/toggle-block")]
        public async Task<IActionResult> ToggleBlock(string userId)
        {
            var result = await _userManagementSerivce.ToggleBlockUserAsync(userId);

            if (!result) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }

        [HttpPatch("users/{userId}/soft-delete")]
        public async Task<IActionResult> SoftDelete(string userId)
        {
            var result = await _userManagementSerivce.ToggleSoftDeleteUserAsync(userId);

            if (!result) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }
    }
}
