using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Waangxuapi.Core.DAL.IHelper;
using wangxuapi.Core.Model;

namespace Waangxuapi.Core.DAL.Helper
{
    public  class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class, new()
    {
        private readonly EFDbContext _DbContext;
        public BaseDAL(EFDbContext _DbContext)
        {
            this._DbContext = _DbContext;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            this._DbContext.Add(entity);
            return await this._DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync()
        {
            //同步
            //return  this._DbContext.Set<TEntity>().ToList();
            //异步方法
            var data = await this._DbContext.Set<TEntity>().ToListAsync();
            return data;
        }
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> GetQueryAsync(Expression<Func<TEntity, bool>> getWhere)
        {
            //同步
            //return  this._DbContext.Set<TEntity>().ToList();
            //异步方法
            var data = await this._DbContext.Set<TEntity>().Where(getWhere).FirstOrDefaultAsync();
            return data;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Del(Expression<Func<TEntity, bool>> delWhere)
        {
            List<TEntity> listDels = await _DbContext.Set<TEntity>().Where(delWhere).ToListAsync();
            listDels.ForEach(model =>
            {
                _DbContext.Entry(model).State = EntityState.Deleted;
            });
            return _DbContext.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> EditAsync(TEntity entity)
        {
            this._DbContext.Entry(entity).State = EntityState.Modified;
            return await this._DbContext.SaveChangesAsync();
        }
    }
}
