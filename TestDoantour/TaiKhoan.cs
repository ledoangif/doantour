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
        [TestCase("", "Test123456")] // email trong
        [TestCase("test02@gmail.com", "")] // mat khau trong
        [TestCase("test02@gmail.com", "123")] // mat khau < 5 ki tu
        [TestCase("test02@gmail.com", "Test123456")] // khong chinh xac
        [TestCase("test02@gmail.com", "Password02")] // hop le
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


        [Test]
        [TestCase("Nguyen A", "0323456789","user06@gmail.com","nguyenA123","1")] // hop le
        [TestCase("Nguyen A", "0323456789","test01@gmail.com","nguyenA123","1")] // email ton tai
        public async Task Register(string Fullname, string Phone,string Email, string Password, int RoleId)
        {
            var registerModel = new RegisterModel
            {
                Fullname = Fullname,
                Phone = Phone,
                Email = Email,
                Password = Password,
                RoleId = RoleId
            };
            var result = await _accountService.Register(registerModel);

            Assert.True(result.Status, result.Message);
        } 
        
        [Test]
        [TestCase("1","Nguyen A", "0323456789", "test01@gmail.com","1")] // hop le
        [TestCase("","Nguyen A", "0323456789","test01@gmail.com","1")] // id trong
        public async Task UpdateUser(string Id,string Fullname, string Phone,string Email, int RoleId)
        {
            var Model = new EditAccountRequest
            {
                Id = Id,
                Phone = Phone,
                Email = Email,
                FullName = Fullname,
                RoleId = RoleId
            };
            var result = await _accountService.UpdateAccount(Id,Model);

            Assert.True(result.Status, result.Message);
        }
        [Test]
        [TestCase("1")] // hop le
        [TestCase("test01")] // khong hop le
        [TestCase("")] // trong
        public async Task DeleteUser(string id)
        {

            var result = await _accountService.DeleteAccount(id);

            Assert.True(result.Status, result.Message);
        }


        [Test]
        [TestCase("test01@gmail.com")] // hop le
        [TestCase("test01")] // khong hop le
        [TestCase("")] // trong
        public async Task GetAccountByEmail(string Email)
        {

            var result = await _accountService.GetAccountByEmail(Email);

            Assert.True(result.Status, result.Message);
        }

    }
}
