
using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.Data.Repository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IBaseRepository : IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns>返回当前对象 - IBaseRepository</returns>
        IBaseRepository BeginTrans();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>返回当前操作的状态</returns>
        int Commit();
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="entity">插入的实体对象</param>
        /// <returns>返回插入的结果</returns>
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="entity">插入的实体集合</param>
        int Insert<TEntity>(List<TEntity> entitys) where TEntity : class;
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="entity">修改的实体对象</param>
        /// <returns>当前修改的状态</returns>
        int Update<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="entity">删除的实体对象</param>
        /// <returns>返回实体删除状态</returns>
        int Delete<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="condition">删除的条件（Lambda表达式）</param>
        /// <returns></returns>
        int Delete<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="keyValue">实体主键</param>
        /// <returns></returns>
        TEntity FindEntity<TEntity>(object keyValue) where TEntity : class;
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="condition">查询的条件（Lambda表达式）</param>
        /// <returns></returns>
        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <returns>返回查询的集合</returns>
        IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class;
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="condition">查询的条件（Lambda表达式）</param>
        /// <returns>返回查询的集合</returns>
        IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        /// <summary>
        /// 根据Sql语句查询数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="strSql">T-SQL语法</param>
        /// <returns>返回查询的集合</returns>
        List<TEntity> FindList<TEntity>(string strSql) where TEntity : class;
        /// <summary>
        /// 根据Sql+参数化语句查询数据
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="strSql">T-SQL语法</param>
        /// <param name="dbParameter">参数列表</param>
        /// <returns>返回查询的集合</returns>
        List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class;
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="pagination">分页对象</param>
        /// <returns>返回查询的集合</returns>
        List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TEntity">当前操作的实体对象</typeparam>
        /// <param name="condition">查询的条件的（Lambda表达式）</param>
        /// <param name="pagination">分页对象</param>
        /// <returns>返回查询的集合</returns>
        List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> condition, Pagination pagination) where TEntity : class, new();
    }
}
