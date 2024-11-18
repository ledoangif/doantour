using Doantour.DTO;
using Doantour.Models;
using Doantour.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public async Task<ResponseModel> Login(AccountLogin loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Email))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Email không được để trống"
                };
            }

            if (string.IsNullOrEmpty(loginModel.Password))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Mật khẩu không được để trống"
                };
            }

            if (!Regex.IsMatch(loginModel.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Vui lòng nhập email hợp lệ"
                };
            }

            if (loginModel.Password.Length < 5)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Mật khẩu phải có ít nhất 5 ký tự"
                };
            }

            if (!Regex.IsMatch(loginModel.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$"))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số"
                };
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Tài khoản không tồn tại!"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: loginModel.Email!,
                password: loginModel.Password!,
                isPersistent: false,
                lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = true,
                    Message = "Đăng nhập thành công"
                };
            }

            return new ResponseModel
            {
                Status = false,
                Message = "Thông tin tài khoản không chính xác"
            };
        }
    }
}
