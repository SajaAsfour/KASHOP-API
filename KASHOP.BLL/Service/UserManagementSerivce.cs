using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
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

        public UserManagementSerivce(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<bool> ChangeRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListResponse>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailsResponse> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleBlockUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
