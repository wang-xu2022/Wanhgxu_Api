using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangxuapi.Core.Model.Model
{
    /// <summary>
    /// 学生实体类型
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public decimal Height { get; set; }
    }
}
