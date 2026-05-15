using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class UserManagementSerivce : IUserManagementSerivce
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementSerivce(UserManager<ApplicationUser> userManager ,IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<bool> ChangeRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if(!roleExists) return false;

            var currentRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRole);

            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public async Task<bool> ToggleSoftDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null) return false;

            user.IsDeleted = !user.IsDeleted;
            user.DeletedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<List<UserListResponse>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Where(u => !u.IsDeleted).ToListAsync();

            return _mapper.Map<List<UserListResponse>>(users);
        }

        public async Task<UserDetailsResponse> GetUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var result = _mapper.Map<UserDetailsResponse>(user);

            result.Role = roles.FirstOrDefault();

            return result;
        }

        public async Task<bool> ToggleBlockUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            bool IsBlocked = user.LockoutEnd > DateTime.UtcNow;

            if (IsBlocked)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(5));
            }
            return true;
        }
    }
}
