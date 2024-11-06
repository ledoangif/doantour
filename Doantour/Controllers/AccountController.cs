using Doantour.Models;
using Doantour.Request;
using Doantour.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace Doantour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(ServiceFactory serviceFactory)
        {
            _service = serviceFactory.AccountService;
        }
        //Thêm tài khoản
        [HttpPost("register")]
        public async Task<ActionResult<ResponseFormat>> Register(RegisterModel account)
        {
            var result = await _service.Register(account);

            if (result.Succeeded)
            {
                return new ResponseFormat(HttpStatusCode.OK, "Thêm tài khoản thành công!", null);
            }
            return BadRequest("Thêm tài khoản thất bại");
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<ActionResult<ResponseFormat>> Login([FromForm] string email, [FromForm] string password)
        {
            var result = await _service.Login(email, password);

            if (result == null)
            {
                return NotFound( "Đăng nhập thất bại");
            }

            return new ResponseFormat(HttpStatusCode.OK, "Đăng nhập thành công", new
            {
                accessToken = result.Token,
                userId = result.UserId,
                userFullName = result.UserFullName,
                email = result.Email,
                role = result.Role
            });

        }
        //Đăng xuất
        [HttpPost("logout")]
        public async Task<ActionResult<ResponseFormat>> Logout()
        {
            var result = await _service.Logout();
            //return Ok(result);
            return new ResponseFormat(HttpStatusCode.NoContent, "Đã đăng xuất!", result);
        }
        // Đổi mật khẩu
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            var result = await _service.ChangePassword(accountId, oldPassword, newPassword);
            if (!result)
            {
                return NotFound("Đổi mật khẩu thất bại");
            }

            return Ok(new { StatusCode = 200, Message = "Đổi mật khẩu thành công!" });
        }
        // Đổi mật khẩu dành cho admin
        [HttpPut("editPassword/{id}")]
        public async Task<ActionResult<ResponseFormat>> PutPassWord(string id, string password)
        {
            var result = await _service.editPassWord(id, password);
            if (!result.Succeeded)
            {
                return NotFound("Đổi mật khẩu thất bại");
            }
            return new ResponseFormat(HttpStatusCode.NoContent, "Đổi mật khẩu thành công!", result);
        }
        //Xóa tài khoản
        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseFormat>> DeleteAccount(string id)
        {
            var result = await _service.DeleteAccount(id);
            if (!result.Succeeded)
            {
                return new ResponseFormat(HttpStatusCode.NoContent, "Xóa tài khoản thất bại!", 0);
            }

            return new ResponseFormat(HttpStatusCode.NoContent, "Xóa tài khoản thành công!", 1);
        }

        // GET: api/Account
        [HttpGet("All")]
        public ActionResult<ResponseFormat> GetAccount()
        {
            var users = _service.GetAllAccount();
            return new ResponseFormat(HttpStatusCode.OK, "List", users);
        }

        [HttpGet("searchByName/{name}")]
        public ActionResult<ResponseFormat> GetAccountsByName(string name)
        {
            var users = _service.GetAccountsByName(name);
            return new ResponseFormat(HttpStatusCode.OK, "List", users);
        }


        // GET: api/Account/email
        [HttpGet("{findbyemail}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountByEmail(string email)
        {
            var users = await _service.GetAccountByEmail(email);
            return new ResponseFormat(HttpStatusCode.OK, "Find Account By Email!", users);
        }

        [HttpGet("findbyid/{id}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountById(string id)
        {
            var user = await _service.GetAccountById(id);
            return new ResponseFormat(HttpStatusCode.OK, "Find Account By Id!", user);
        }

        [HttpGet("getId/{email}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountId(string email)
        {
            var userId = await _service.GetAccountId(email);

            return new ResponseFormat(HttpStatusCode.OK, "Get User Id!", userId);
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseFormat>> PutAccount(string id, EditAccountRequest account)
        {
            var result = await _service.UpdateAccount(id, account);
            if (!result.Succeeded)
            {
                return NotFound("Cập nhật thất bại!");
            }

            return new ResponseFormat(HttpStatusCode.NoContent, "Cập nhật thành công!", result);
        }



        

    }
}
