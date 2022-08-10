using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangxuapi.Core.Common.Helper;

namespace Wangxuapi.Core.DAL.DBCore
{
    public class ConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public static string conn_wx { get { return AppSettings.App(new string[] { "AppSettings", "ConnectionString" }); } }
    }
}
