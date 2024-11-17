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
            new AppUser { Email = "test01@gmail.com", UserName = "User01" },
            new AppUser { Email = "test02@gmail.com", UserName = "User02" },
            new AppUser { Email = "test03@gmail.com", UserName = "User03" }
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

        public override Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            if (user == null) return Task.FromResult(false);
            var isPasswordValid = _passwords.ContainsKey(user.Email) && _passwords[user.Email] == password;
            return Task.FromResult(isPasswordValid);
        }
    }

}
