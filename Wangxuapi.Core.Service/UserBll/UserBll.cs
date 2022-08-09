using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waangxuapi.Core.DAL.IHelper;
using Wangxuapi.Core.Common.Helper;
using Wangxuapi.Core.Common.MD5;
using Wangxuapi.Core.Model;
using Wangxuapi.Core.Model.Common;
using Wangxuapi.Core.Model.Model;

namespace Wangxuapi.Core.Service.UserBll
{
    public class UserBll
    {
        public ILogger<UserBll> _Logger;

        private readonly ICityDAL _cityDAL;
        public UserBll(ICityDAL cityDAL)
        {
            this._cityDAL = cityDAL;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public async Task<ApiResultTypeBasic<Token>>GetUserList(string Account,string Password)
        {
            ApiResultTypeBasic<Token> result = new ApiResultTypeBasic<Token>() { code = -200,msg="查询失败",data=null };
            try
            {
                Token tokenData=new Token();
                var data = await this._cityDAL.GetQueryAsync(c => c.Account == Account && c.Password == SafetyHelper.MD5Hex(Password)&&c.Status==1);
                if (data != null)
                {

                    bool suc = false;
                    string jwtStr = string.Empty;
                    // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                    TokenModel tokenModel = new TokenModel
                    {
                        UserId = data.UserId,
                        Account = data.Account,
                        Name = data.Name,
                        Status = data.Status,
                        CreatDate = data.CreatDate,
                    };
                    jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
                    suc = true;
                    #region 获取payload信息
                    var handler = new JwtSecurityTokenHandler();
                    var payload = handler.ReadJwtToken(jwtStr).Payload;
                    var claims = payload.Claims;
                    var expireTime = claims.First(claim => claim.Type == "exp").Value;
                    #endregion
                    tokenData.success = suc; tokenData.token = jwtStr; tokenData.UserId=data.UserId;tokenData.Name = data.Name; tokenData.Status = data.Status;
                    tokenData.CreatDate = data.CreatDate; tokenData.Account = data.Account;tokenData.Age = data.Age;
                    result.code = 200;
                    result.msg = "登录成功";
                    result.data = tokenData;
                    return result;
                }
                else
                {
                    result.code = -200;
                    result.msg = "登录失败，账号密码错误";
                    result.data = tokenData;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _Logger.LogInformation("登录接口接口"+ex.ToString());
                return result; ;
            }
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResultRoot> CreateUser(User user)
        {
            ApiResultRoot result=new ApiResultRoot() { msg="添加失败",code=-200};
            try
            {
                if(string.IsNullOrWhiteSpace(user.Account))
                {
                    result.msg = "账号不能为空";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    result.msg = "姓名不能为空";
                    return result;
                }
                if(string.IsNullOrEmpty(user.Password))
                {
                    result.msg = "密码不能为空";
                    return result;
                }
                user.CreatDate=DateTime.Now;//默认时间
                user.Status = 1;//状态
                user.Password= SafetyHelper.MD5Hex(user.Password);
                int list = await this._cityDAL.Add(user);
                if (list > 0)
                {
                    result.code = 200;
                    result.msg = "添加成功";
                    return result;
                }
                return result;
            }
            catch (Exception ex )
            {
                _Logger.LogInformation("创建用户" + ex.ToString());
                return result;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId">删除用户</param>
        /// <returns></returns>
        public async Task<ApiResultRoot> DeleteUser(int userId)
        {
            ApiResultRoot result = new ApiResultRoot() { code=-200,msg="删除失败"};
            try
            {
                int list = await this._cityDAL.Del(c => c.UserId == userId);
                if(list > 0)
                {
                    result.code=200;
                    result.msg = "删除成功";
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                _Logger.LogInformation("删除用户接口" + ex.ToString());
                return result;
            }
        }
    }
}
