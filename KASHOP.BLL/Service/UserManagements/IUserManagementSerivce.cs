using KASHOP.DAL.DTO.Response.UserManagements;

namespace KASHOP.BLL.Service.UserManagements
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
