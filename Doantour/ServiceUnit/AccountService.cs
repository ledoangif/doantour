using Doantour.DTO;
using Doantour.Models;
using Doantour.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
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

        public async Task<ResponseModel> Register(RegisterModel account)
        {
            if (string.IsNullOrEmpty(account.Fullname) || string.IsNullOrEmpty(account.Phone) || string.IsNullOrEmpty(account.Email) || string.IsNullOrEmpty(account.Password))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin này không được để trống"
                };
            }
            if (!Regex.IsMatch(account.Fullname, @"^[A-Za-zÀ-ỹ\s]+$"))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Họ tên chỉ được chứa ký tự chữ cái."
                };
            }
            if (!Regex.IsMatch(account.Phone, @"^((03|05|07|08|09|01[2|6|8|9])\d{8}|\+84\d{9})$"))
            {
                                return new ResponseModel
                {
                    Status = false,
                    Message = "Số điện thoại không đúng"
                                };
                
            }
            // Kiểm tra nếu email bị thiếu hoặc không hợp lệ
            if (!Regex.IsMatch(account.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                                return new ResponseModel
                {
                    Status = false,
                    Message = "Vui lòng nhập một email hợp lệ"
                                };
                
            }
            // Kiểm tra nếu mật khẩu bị thiếu hoặc không đủ điều kiện
            if (account.Password.Length < 5)
            {
                                return new ResponseModel
                {
                    Status = false,
                    Message = "Mật khẩu phải có ít nhất 5 ký tự"
                                };
                
            }
            if (!Regex.IsMatch(account.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$"))
            {
                                return new ResponseModel
                {
                    Status = false,
                    Message = "Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số"
                                };
                
            }
            // Kiểm tra tài khoản đã tồn tại trong cơ sở dữ liệu chưa
            var existingUser = await _userManager.FindByEmailAsync(account.Email);
            if (existingUser != null)
            {
                                return new ResponseModel
                {
                    Status = false,
                    Message = "Email này đã tồn tại."
                                };
                
            }

            var user = new AppUser()
            {
                FullName = account.Fullname,
                Phone = account.Phone,
                PhoneNumber = account.Phone,
                RoleId = account.RoleId,
                UserName = account.Email,
                Email = account.Email,
                PasswordHash = account.Password,
            };

            var result = await _userManager.CreateAsync(user, user.PasswordHash!);

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

        public async Task<ResponseModel> UpdateAccount(string id, EditAccountRequest account)
        {
                if (string.IsNullOrEmpty(account.Id))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        Message = "Id không bỏ trống"
                    };
                }

                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        Message = "Không tìm thấy người dùng"
                    };
                }

                var result = await _userManager.UpdateAsync(user);

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

        public async Task<ResponseModel> GetAccountByEmail(string email)
        {
            // Kiểm tra nếu email bị trống
            if (string.IsNullOrEmpty(email))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Vui lòng nhập email"
                };

            }
            // Kiểm tra nếu email bị thiếu hoặc không hợp lệ
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Vui lòng nhập một email hợp lệ"
                };

            }
            var users = await _userManager.FindByEmailAsync(email);

            if (users != null)
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

        public async Task<ResponseModel> DeleteAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Id không bỏ trống"
                };
            }

            var account = await _userManager.FindByIdAsync(id);
            if (account == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Thông tin tài khoản không tồn tại"
                };
            }

            var result = await _userManager.DeleteAsync(account);

            if (result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = true,
                    Message = "Xoá thành công"
                };
            }

            return new ResponseModel
            {
                Status = false,
                Message = "Xoá thất bại"
            };
        }
    }
}
