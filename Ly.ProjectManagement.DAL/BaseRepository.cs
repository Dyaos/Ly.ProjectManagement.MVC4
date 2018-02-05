using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Code;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Text.RegularExpressions;
using Ly.ProjectManagement.Model;

namespace Ly.ProjectManagement.Data.Repository
{
    public class BaseRepository : IBaseRepository, IDisposable
    {
        private ProjectManagement_DBEntities dbContext = DbContextFactory.GetCurrentDbContext();//获取上下文
        private DbTransaction dbTransaction { get; set; }//当前事务对象


        public IBaseRepository BeginTrans()
        {
            DbConnection dbConnection = ((IObjectContextAdapter)dbContext).ObjectContext.Connection;//获取数据库连接对象
            if (dbConnection.State == System.Data.ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }

        public int Commit()
        {
            try
            {
                var returnValue = dbContext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return returnValue;
            }
            catch (Exception)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Dispose();
            }
        }

        public int Delete<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Deleted;
            return dbTransaction == null ? this.Commit() : 0;
        }

        public int Delete<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            var entitys = dbContext.Set<TEntity>().Where(condition).ToList();
            entitys.ForEach(m => dbContext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbTransaction == null ? this.Commit() : 0;
        }


        public TEntity FindEntity<TEntity>(object keyValue) where TEntity : class
        {
            return dbContext.Set<TEntity>().Find(keyValue);
        }

        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return dbContext.Set<TEntity>().FirstOrDefault(condition);
        }

        public List<TEntity> FindList<TEntity>(string strSql) where TEntity : class
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql).ToList();
        }

        public List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList();
        }

        public List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;//是否升序
            string[] order = pagination.sidx.Split(',');//获取排序字段组合
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in order)
            {
                string orderPart = item;//获取单个排序字段
                orderPart = Regex.Replace(orderPart, @"\s+", " ");
                string[] orderArray = orderPart.Split(' ');
                string orderFiled = orderArray[0];
                bool sort = isAsc;
                if (orderArray.Length == 2)
                {
                    isAsc = orderArray[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(orderFiled);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }

        public List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> condition, Pagination pagination) where TEntity : class, new()
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;//是否升序
            string[] order = pagination.sidx.Split(',');//获取排序字段组合
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<TEntity>().Where(condition).AsQueryable();
            foreach (var item in order)
            {
                string orderPart = item;//获取单个排序字段
                orderPart = Regex.Replace(orderPart, @"\s+", " ");
                string[] orderArray = orderPart.Split(' ');
                string orderFiled = orderArray[0];
                bool sort = isAsc;
                if (orderArray.Length == 2)
                {
                    isAsc = orderArray[1].ToUpper() == "ASC" ? true : false;
                }
                
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(orderFiled);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }

        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            return dbTransaction == null ? this.Commit() : 0;
        }

        public int Insert<TEntity>(List<TEntity> entitys) where TEntity : class
        {
            foreach (var item in entitys)
            {
                dbContext.Entry<TEntity>(item).State = EntityState.Added;
            }
            return dbTransaction == null ? this.Commit() : 0;
        }

        public IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return dbContext.Set<TEntity>().Where(condition).AsQueryable();
        }

        public int Update<TEntity>(TEntity entity) where TEntity : class
        {
            
            dbContext.Set<TEntity>().Attach(entity);//在 TEntity 实体表上附上实体 entity
            PropertyInfo[] props = entity.GetType().GetProperties();//获取属性
            foreach (var prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbContext.Entry<TEntity>(entity).Property(prop.Name).CurrentValue = null;
                    if (prop.PropertyType.Name != "ICollection`1")
                        dbContext.Entry<TEntity>(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
    }
}
