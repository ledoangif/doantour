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
        private FakeSignInManager _signInManagerMock;
        private AccountService _accountService;
        private CountryService _countryService;

        [SetUp]

        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Hachutravelcontext>()
                 .UseSqlServer("Data Source=(local)\\SQLSV2019EXP;User ID=sa;Password=SqlSv2019;Initial Catalog=Hachutour;Integrated Security=True;Trust Server Certificate=True")
                 .Options;
            _context = new Hachutravelcontext(options);
            //repo

            var fakeUserManager = new FakeUserManager();

            // Sử dụng FakeSignInManager
            var httpContextMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext(); // Tạo một HttpContext giả lập
            httpContextMock.Setup(x => x.HttpContext).Returns(httpContext);

            _signInManagerMock = new FakeSignInManager(fakeUserManager, httpContextMock.Object);

            //
            _accountService = new AccountService(_context, fakeUserManager, _signInManagerMock);
            _countryService = new CountryService(_context);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}