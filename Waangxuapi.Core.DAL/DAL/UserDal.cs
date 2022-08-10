using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangxuapi.Core.Common.Helper;
using Wangxuapi.Core.DAL.DBCore;
using Wangxuapi.Core.Model.Model;

namespace Wangxuapi.Core.DAL.DAL
{
    public class UserDal
    {
        public static User GetList()
        {
            string sql = "SELECT * FROM [User]";
            return DBHelper.ExecuteSql_First<User>(ConnectionStrings.conn_wx,sql,null);
        }
    }
}
