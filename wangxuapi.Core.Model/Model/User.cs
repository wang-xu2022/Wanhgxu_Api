using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangxuapi.Core.Model.Model
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(50)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 启用状态 0 停用 1 启用
        /// </summary>
        public int Status { get; set; }
    }
    public class Token:User
    {
        /// <summary>
        /// 成功值
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
    }
}
