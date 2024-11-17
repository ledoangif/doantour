using AutoMapper;
using Doantour.DTO;
using Doantour.Models;
using Doantour.Repository;
using Doantour.Request;
using Doantour.Respository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace Doantour.ServiceUnit
{
    public class AccountService
    {
        private readonly Hachutravelcontext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(Hachutravelcontext hachutravelcontext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = hachutravelcontext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResponseDTO> Login(string email, string password)
        {
            if (email == null || email.Length < 1)
            {
                return new AuthResponseDTO
                {
                    Status = false,
                    Message = "Email khong duoc de trong"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: email!,
                password: password!,
                isPersistent: false,
                lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                return new AuthResponseDTO
                {
                    Status = true,
                    Message = "Đăng nhập thành công"
                };
            }

            return new AuthResponseDTO
            {
                Status = false,
                Message = "Thông tin tài khoản không chính xác"
            };
        }
    }
}
