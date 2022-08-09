using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangxuapi.Core.Model.Common
{
    /// <summary>
    /// 最简状态 code,msg
    /// </summary>
    public class ApiResultRoot
    {
        /// <summary>
        /// 状态码 200 成功 0,-200 异常
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回消息描述
        /// </summary>
        public string msg { get; set; }
    }
    /// <summary>
    /// 带data的api结果结构
    /// </summary>
    public class ApiResultBasic : ApiResultRoot
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

    /// <summary>
    /// 带data的api结果结构
    /// </summary>
    public class ApiResultData<T> : ApiResultRoot
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 指定类型的
    /// </summary>
    public class ApiResultTypeBasic<T> : ApiResultRoot
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 分页
    /// </summary>
    public class ApiResultPager : ApiResultBasic
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPageNum { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
    }
    /// <summary>
    /// 分页
    /// </summary>
    public class ApiResultTypePager<T> : ApiResultTypeBasic<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPageNum { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
    }
    /// <summary>
    /// 命名更合理
    /// </summary>
    public class ApiResultPagerBetter : ApiResultBasic
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPageCount { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
    }
    /// <summary>
    /// Api返回结果,指定类型
    /// </summary>
    public class ApiTResult<T> : ApiResultRoot
    {
        /// <summary>
        /// data
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 分页返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageTResult<T> : ApiResultRoot
    {
        /// <summary>
        /// data
        /// </summary>
        public PageT<T> data { get; set; }
    }
    /// <summary>
    /// 分页实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageT<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int dataCount { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageCount { get; set; }
        /// <summary>
        /// 返回内容
        /// </summary>
        public T data { get; set; }
    }
    public class ApiDataTResult<T> : ApiResultRoot
    {
        /// <summary>
        /// data
        /// </summary>
        public DataT<T> data { get; set; }
    }
    public class DataT<T>
    {
        /// <summary>
        /// 返回内容
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerResultData<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> list { get; set; }
        /// <summary>
        /// ElasticSearch地址
        /// </summary>
        public string esUrl { get; set; }
    }
}
