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
            new AppUser { Id = "1", Email = "test01@gmail.com", UserName = "User01" },
            new AppUser { Id = "2", Email = "test02@gmail.com", UserName = "User02" },
            new AppUser { Id = "3", Email = "test03@gmail.com", UserName = "User03" }
        };

            _passwords = new Dictionary<string, string>
        {
            { "test01@gmail.com", "Password01" },
            { "test02@gmail.com", "Password02" },
            { "test03@gmail.com", "Password03" }
        };
        }

        public override Task<AppUser> FindByIdAsync(string id)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }
        
        public override Task<AppUser> FindByEmailAsync(string email)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }   
        
        public override Task<AppUser> FindByNameAsync(string name)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.FullName == name);
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
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(password))
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Description = "Password cannot be empty."
                }));

            if (_fakeUsers.Any(u => u.Email == user.Email))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Description = "A user with this email already exists."
                }));
            }

            _fakeUsers.Add(user);
            _passwords[user.Email] = password;

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> UpdateAsync(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = _fakeUsers.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found."
                }));
            }

            // Cập nhật thông tin người dùng
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email; // Giả sử email có thể được cập nhật

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> DeleteAsync(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = _fakeUsers.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found."
                }));
            }

            _fakeUsers.Remove(existingUser);
            _passwords.Remove(existingUser.Email);

            return Task.FromResult(IdentityResult.Success);
        }
    }

}
