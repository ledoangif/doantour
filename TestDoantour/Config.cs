using AutoMapper;
using Doantour.Models;
using Doantour.Repository;
using Doantour.Respository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Moq;
using Microsoft.AspNetCore.Http;
using Doantour.DTO;
using Doantour.Controllers;
using Doantour.ServiceUnit;

namespace TestDoantour
{
    [TestFixture]
    public partial class Tests
    {
        private Hachutravelcontext _context;
        private Mock<UserManager<AppUser>> _userManagerMock;
        private FakeSignInManager _signInManagerMock;
        private AccountService _accountService;

        [SetUp]

        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Hachutravelcontext>()
                 .UseSqlServer("Data Source=(local)\\SQLSV2019EXP;User ID=sa;Password=SqlSv2019;Initial Catalog=AppTest.Test;Integrated Security=True;Trust Server Certificate=True")
                 .Options;
            _context = new Hachutravelcontext(options);
            //repo

            _userManagerMock = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            // Sử dụng FakeSignInManager
            var httpContextMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext(); // Tạo một HttpContext giả lập
            httpContextMock.Setup(x => x.HttpContext).Returns(httpContext);

            _signInManagerMock = new FakeSignInManager(_userManagerMock.Object, httpContextMock.Object);

            //
            _accountService = new AccountService(_context,_userManagerMock.Object,_signInManagerMock);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}