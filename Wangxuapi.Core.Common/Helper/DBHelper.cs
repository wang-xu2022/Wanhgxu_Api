using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangxuapi.Core.Common.Helper
{
    public class DBHelper
    {
        //连接数据库字符串。
        private static string cnnstr = "";

        public DBHelper()
        {
            cnnstr = "";
        }

        private static SqlConnection GetConnection()
        {
            SqlConnection cnn = new SqlConnection(cnnstr);
            cnn.Open();
            return cnn;
        }
        private static SqlConnection GetConnection(string conn)
        {
            SqlConnection cnn = new SqlConnection(conn);
            cnn.Open();
            return cnn;
        }
        /// <summary>
        /// 执行SQL 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteSql(string conn, string sql, object param)
        {
            using (var con = GetConnection(conn))
            {
                int i = con.Execute(sql, param);
                return i;
            }
        }


        /// <summary>
        /// 执行SQL，返回第一行第一个元素的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteSql_First<T>(string conn, string sql, object param)
        {
            using (var con = GetConnection(conn))
            {
                T result = con.Query<T>(sql, param).FirstOrDefault<T>();
                return result;
            }
        }

        /// <summary>
        /// 执行SQL，返回数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<T> ExecuteSQL_ToList<T>(string conn, string sql, object param)
        {
            using (var con = GetConnection(conn))
            {
                IEnumerable<T> result = con.Query<T>(sql, param);
                return result.ToList<T>();
            }
        }
        /// <summary>
        /// 执行存储过程,无返回结果集  
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteSP(string conn, string proName, object param)
        {
            using (var con = GetConnection(conn))
            {
                int result = con.Execute(proName, param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// 执行存储过程,返回数据实体结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<T> ExecuteSP_ToList<T>(string conn, string proName, object param)
        {
            using (var con = GetConnection(conn))
            {
                IEnumerable<T> result = con.Query<T>(proName, param, null, false, null, CommandType.StoredProcedure);
                return result.ToList<T>();
            }
        }
        public static ArrayList ExecuteSP_ToArrayList(string conn, string proName, object param)
        {
            using (var con = GetConnection(conn))
            {
                IEnumerable<Dictionary<string, object>> result = con.Query<Dictionary<string, object>>(proName, param, null, false, null, CommandType.StoredProcedure);
                ArrayList a = new ArrayList();
                if (result != null && result.Count() > 0)
                {
                    Dictionary<string, object> item1 = result.ToList<Dictionary<string, object>>()[0];
                    foreach (object o in item1.Values.ToArray())
                    {
                        a.Add(o);
                    }
                }
                return a;
            }
        }
        public static ArrayList ExecuteSql_ToArrayList(string conn, string sqlName, object param)
        {
            using (var con = GetConnection(conn))
            {
                IEnumerable<Dictionary<string, object>> result = con.Query<Dictionary<string, object>>(sqlName, param, null, false, null, CommandType.Text);
                ArrayList a = new ArrayList();
                if (result != null && result.Count() > 0)
                {
                    Dictionary<string, object> item1 = result.ToList<Dictionary<string, object>>()[0];
                    foreach (object o in item1.Values.ToArray())
                    {
                        a.Add(o);
                    }
                }
                return a;
            }
        }
        public static string ExecuteSP_GetField<T>(string conn, string proName, object param)
        {
            using (var con = GetConnection(conn))
            {
                IEnumerable<string> result = con.Query<string>(proName, param, null, false, null, CommandType.StoredProcedure);
                if (result.Count() > 0)
                {
                    return result.ToList<string>()[0];
                }
                else return "";

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="proName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteSp_First<T>(string conn, string proName, object param)
        {
            using (var con = GetConnection(conn))
            {
                T result = con.Query<T>(proName, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault<T>();
                return result;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> ExecuteSqlToList<T>(string conn, string sql, object param)
        {

            List<T> result = null;

            IEnumerable<T> resultMidle = null;
            using (var con = GetConnection(conn))
            {
                resultMidle = con.Query<T>(sql, param);
                if (resultMidle != null) result = resultMidle.ToList<T>();
            }
            return result;
        }
        public static List<T> ExecuteSpToList<T>(string conn, string proName, object param)
        {
            List<T> result = null;

            IEnumerable<T> resultMidle = null;
            using (var con = GetConnection(conn))
            {
                resultMidle = con.Query<T>(proName, param, null, false, null, CommandType.StoredProcedure);
                if (resultMidle != null) result = resultMidle.ToList<T>();
            }
            return result;
        }
    }
}
