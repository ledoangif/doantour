using Doantour.Controllers;
using Doantour.Repository;
using Doantour.Request;
using Doantour.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoantour
{
    public partial class Tests
    {
        [Test]
        [TestCase("1","2")]
        [TestCase("1","1")]
        [TestCase("1","3")]
        public void SampleTest(int a, int b)
        {
            var result = (a + b) == 2;
            var message = $"Khong bang nhau. a={a}, b={b}";
            Assert.True(result, message);
        }


        [Test]
        [TestCase("", "Test123456")]
        [TestCase("test02@gmail.com", "")]
        [TestCase("test02@gmail.com", "Test123456")]
        [TestCase("doanhaile2002721@gmail.com", "Test123456")]
        public async Task Login(string email, string password)
        {
            var loginModel = new AccountLogin
            {
                Email = email,
                Password = password
            };
            var result = await _accountService.Login(loginModel);

            Assert.True(result.Status,result.Message);
        }

    }
}
