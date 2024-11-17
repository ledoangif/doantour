using Doantour.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    public class FakeSignInManager : SignInManager<AppUser>
    {
        public FakeSignInManager(
                UserManager<AppUser> userManager,
                IHttpContextAccessor contextAccessor
            ) : base(
                userManager,
                contextAccessor,
                new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<AppUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<AppUser>>().Object
            )
        { }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            // Giả lập danh sách người dùng và mật khẩu
            var users = new Dictionary<string, string>
            {
                { "test01@gmail.com", "Password01" },
                { "test02@gmail.com", "Password02" },
                { "test03@gmail.com", "Password03" }
            };

            // Kiểm tra xem thông tin đăng nhập có đúng không
            if (users.ContainsKey(userName) && users[userName] == password)
            {
                return Task.FromResult(SignInResult.Success);
            }

            return Task.FromResult(SignInResult.Failed);
        }

        public override Task SignOutAsync()
        {
            // Giả lập hành vi SignOut
            return Task.CompletedTask;
        }
    }
}
