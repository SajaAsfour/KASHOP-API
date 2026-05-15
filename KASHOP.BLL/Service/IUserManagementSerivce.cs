using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IUserManagementSerivce
    {
        Task<List<UserListResponse>> GetAllUsersAsync();
        Task<UserDetailsResponse> GetUserAsync(string userId);
        Task<bool> ChangeRoleAsync(string userId, string role);
        Task<bool> ToggleBlockUserAsync(string userId);
        Task<bool> ToggleSoftDeleteUserAsync(string userId);
    }
}
