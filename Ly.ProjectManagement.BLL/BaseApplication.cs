using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Data.Repository;

namespace Ly.ProjectManagement.Data.Application
{
    public abstract class BaseApplication : IBaseApplication
    {
        public IBaseRepository CurrentRepository { get; set; }

        /// <summary>
        /// 仓储工厂
        /// </summary>
        public int Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return CurrentRepository.Delete<TEntity>(entity);
        }

        public int Delete<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return CurrentRepository.Delete<TEntity>(condition);
        }

        public List<TEntity> FindList<TEntity>(string strSql) where TEntity : class
        {
            return CurrentRepository.FindList<TEntity>(strSql);
        }

        public List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class
        {
            return CurrentRepository.FindList<TEntity>(strSql, dbParameter);
        }

        public List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
        {
            return CurrentRepository.FindList<TEntity>(pagination);
        }

        public List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> condition, Pagination pagination) where TEntity : class, new()
        {
            return CurrentRepository.FindList<TEntity>(condition, pagination);
        }

        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            return CurrentRepository.Insert<TEntity>(entity);
        }

        public int Insert<TEntity>(List<TEntity> entitys) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class
        {
            return CurrentRepository.IQueryable<TEntity>();
        }

        public IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return CurrentRepository.IQueryable<TEntity>(condition);
        }

        public int Update<TEntity>(TEntity entity) where TEntity : class
        {
            return CurrentRepository.Update<TEntity>(entity);
        }

        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return CurrentRepository.FindEntity<TEntity>(condition);
        }
    }
}
