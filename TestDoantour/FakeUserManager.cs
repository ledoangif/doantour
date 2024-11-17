using Doantour.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoantour
{

    public class FakeUserManager : UserManager<AppUser>
    {
        private readonly List<AppUser> _fakeUsers;
        private readonly Dictionary<string, string> _passwords;
        private readonly Dictionary<string, string> _passwordResetTokens = new();

        public FakeUserManager() : base(
            new Mock<IUserStore<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new PasswordHasher<AppUser>(), // Sử dụng PasswordHasher thực nếu cần
            new IUserValidator<AppUser>[0],
            new IPasswordValidator<AppUser>[0],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<AppUser>>>().Object)
        {
            _fakeUsers = new List<AppUser>
        {
            new AppUser {Id = "1", Email = "test01@gmail.com", UserName = "User01" },
            new AppUser {Id = "2", Email = "test02@gmail.com", UserName = "User02" },
            new AppUser {Id = "3", Email = "test03@gmail.com", UserName = "User03" }
        };

            _passwords = new Dictionary<string, string>
        {
            { "test01@gmail.com", "Password01" },
            { "test02@gmail.com", "Password02" },
            { "test03@gmail.com", "Password03" }
        };
        }

        public override Task<AppUser> FindByEmailAsync(string email)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }
        public override Task<AppUser> FindByIdAsync(string id)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public override Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            if (user == null) return Task.FromResult(false);
            var isPasswordValid = _passwords.ContainsKey(user.Email) && _passwords[user.Email] == password;
            return Task.FromResult(isPasswordValid);
        }

        public override Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            // Kiểm tra nếu email đã tồn tại thì trả về lỗi
            if (_fakeUsers.Any(u => u.Email == user.Email))
            {
                var errors = new List<IdentityError>
            {
                new IdentityError { Description = "Email already exists." }
            };
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            // Giả lập việc thêm người dùng mới
            _fakeUsers.Add(user);
            _passwords[user.Email] = password; // Lưu mật khẩu vào danh sách

            return Task.FromResult(IdentityResult.Success); // Trả về thành công
        }

        public override Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            if (user == null || !_passwords.ContainsKey(user.Email))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User not found." }));
            }

            // Kiểm tra mật khẩu hiện tại có chính xác không
            var isCurrentPasswordValid = _passwords[user.Email] == currentPassword;
            if (!isCurrentPasswordValid)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Current password is incorrect." }));
            }

            // Thay đổi mật khẩu
            _passwords[user.Email] = newPassword; // Cập nhật mật khẩu mới vào từ điển

            // Trả về kết quả thành công
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            // Tạo một token giả (ví dụ: GUID)
            var token = Guid.NewGuid().ToString();

            // Lưu token vào từ điển (giả lập lưu trữ token)
            _passwordResetTokens[user.Email] = token;

            return Task.FromResult(token);  // Trả về token giả
        }

        // Giả lập ResetPasswordAsync
        public override Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            // Kiểm tra xem token có hợp lệ hay không (giả lập)
            if (_passwordResetTokens.ContainsKey(user.Email) && _passwordResetTokens[user.Email] == token)
            {
                // Giả lập đặt lại mật khẩu thành công
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                // Nếu token không hợp lệ
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));
            }
        }

        public override Task<IdentityResult> DeleteAsync(AppUser user)
        {
            // Kiểm tra người dùng có tồn tại trong danh sách giả lập không
            var existingUser = _fakeUsers.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Nếu người dùng tồn tại, giả lập xóa người dùng
                _fakeUsers.Remove(existingUser);
                return Task.FromResult(IdentityResult.Success);  // Xóa thành công
            }
            else
            {
                // Nếu người dùng không tồn tại, trả về lỗi
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User not found" }));
            }
        }
    }

}
