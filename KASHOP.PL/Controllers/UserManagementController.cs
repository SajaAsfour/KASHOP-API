using KASHOP.BLL.Service;
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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManagementSerivce.GetAllUsersAsync();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = users
            });
        }
    }
}
