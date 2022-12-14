using Microsoft.AspNetCore.Mvc;
using Wangxuapi.Core.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Wangxuapi.Core.Service.UserBll;
using Wangxuapi.Core.Model.Common;
using Wangxuapi.Core.Model.Model;

namespace Wanhgxu_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _Logger;
        private readonly UserBll _userBll;
        public UserController(UserBll userBll)
        {
            this._userBll = userBll;
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <param name="Name">姓名</param>
        /// <param name="Age">年龄</param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<ApiResultRoot> CreateUser([FromForm] string Account, [FromForm] string Password, [FromForm] string Name, [FromForm] int Age)
        {
            return await this._userBll.CreateUser(Account, Password, Name,Age);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<ApiResultTypeBasic<Token>> Login(string Account,string Password)
        {
            //_Logger.LogInformation("登录日志测试");
            var data = await this._userBll.GetUserList(Account,Password);
            return data;
        }
        /// <summary>
        /// 解析Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("ParseToken")]
        public IActionResult ParseToken()
        {
            //需要截取Bearer 
            var tokenHeader = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = JwtHelper.SerializeJwt(tokenHeader);
            return Ok(user);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpGet("DeleteUser")]
        public async Task<object> DeleteUser(int userId)
        {
            return await this._userBll.DeleteUser(userId);
        }
        [HttpGet("GerUserListssss")]
        [AllowAnonymous]
        public  User GerUserListssss()
        {
            return UserBll.GerUserListssss();
        }
    }
}
