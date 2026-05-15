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

        public UserManagementSerivce(UserManager<ApplicationUser> userManager ,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Task<bool> ChangeRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserListResponse>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return _mapper.Map<List<UserListResponse>>(users);
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
