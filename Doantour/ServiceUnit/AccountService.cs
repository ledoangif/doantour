//using AutoMapper;
//using Doantour.DTO;
//using Doantour.Models;
//using Doantour.Repository;
//using Doantour.Request;
//using Doantour.Respository;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.VisualStudio.Services.Users;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Text.RegularExpressions;
//using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

//namespace Doantour.ServiceUnit
//{
//    public class AccountService
//    {
//        private readonly Hachutravelcontext _context;
//        private readonly UserManager<AppUser> _userManager;
//        private readonly SignInManager<AppUser> _signInManager;

//        public AccountService(Hachutravelcontext hachutravelcontext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
//        {
//            _context = hachutravelcontext;
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        public async Task<AuthResponseDTO> Login(AccountLogin loginModel)
//        {
//            if (string.IsNullOrEmpty(loginModel.Email))
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Email không được để trống"
//                };
//            }

//            if (string.IsNullOrEmpty(loginModel.Password))
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Mật khẩu không được để trống"
//                };
//            }

//            if (!Regex.IsMatch(loginModel.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Vui lòng nhập email hợp lệ"
//                };
//            }

//            if (loginModel.Password.Length < 5)
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Mật khẩu phải có ít nhất 5 ký tự"
//                };
//            }

//            if (!Regex.IsMatch(loginModel.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$"))
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số"
//                };
//            }

//            var user = await _userManager.FindByEmailAsync(loginModel.Email);
//            if (user == null)
//            {
//                return new AuthResponseDTO
//                {
//                    Status = false,
//                    Message = "Tài khoản không tồn tại!"
//                };
//            }

//            var result = await _signInManager.PasswordSignInAsync(
//                userName: loginModel.Email!,
//                password: loginModel.Password!,
//                isPersistent: false,
//                lockoutOnFailure: false
//            );
//            if (result.Succeeded)
//            {
//                return new AuthResponseDTO
//                {
//                    Status = true,
//                    Message = "Đăng nhập thành công"
//                };
//            }

//            return new AuthResponseDTO
//            {
//                Status = false,
//                Message = "Thông tin tài khoản không chính xác"
//            };
//        }
//    }
//}
