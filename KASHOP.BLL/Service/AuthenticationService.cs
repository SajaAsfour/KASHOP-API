using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationService (UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Error"
                };
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new RegisterResponse()
            {
                Success = true,
                Message = "Success"
            };
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Invalid Email"
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Invalid Password"
                };
            }
            return new LoginResponse()
            {
                Success = true,
                Message = "Success"
            };
        }
    }
}
