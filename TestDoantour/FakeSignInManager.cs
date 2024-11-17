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
            // Giả lập kết quả đăng nhập thành công
            return Task.FromResult(SignInResult.Success);
        }

        public override Task SignOutAsync()
        {
            // Giả lập hành vi SignOut
            return Task.CompletedTask;
        }
    }
}
