using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Waangxuapi.Core.DAL.IHelper
{
    public  interface IBaseDAL<TEntity> where TEntity : class,new ()
    {
        //查询表全部数据     
        public Task<List<TEntity>> QueryAsync();
        //查询全部数据
        public Task<TEntity> GetQueryAsync(Expression<Func<TEntity, bool>> getWhere);
        //新增
        public Task<int> Add(TEntity entity);
        //删除
        public Task<int> Del(Expression<Func<TEntity, bool>> delWhere);
        //修改
        public Task<int> EditAsync(TEntity entity);
    }
}
