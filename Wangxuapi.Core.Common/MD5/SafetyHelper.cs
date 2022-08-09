using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wangxuapi.Core.Common.MD5
{
    /// <summary>
    /// 安全加密
    /// </summary>
    public class SafetyHelper
    {
        public const string DESKEY = "tmd432ndfdsf232965r";
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5Hex(string text)
        {
            var bytes = MD5(text);
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static byte[] MD5(string text)
        {
            var provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var result = provider.ComputeHash(bytes);
            return result;
        }
        /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
        /// <summary>
        /// 生产整数随机码，开头不是0
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string getRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                if (i == 0)
                {
                    result.Append(r.Next(1, 10));
                }
                else result.Append(r.Next(0, 10));

            }
            return result.ToString();
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="sDecrKey"></param>
        /// <returns></returns>
        public static string DESDecrypt(string inputText, string sDecrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[inputText.Length];
            try
            {
                byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(inputText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="strEncrKey"></param>
        /// <returns></returns>
        public static string DESEncrypt(string inputText, string strEncrKey)
        {
            byte[] byKey = { };
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(inputText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
