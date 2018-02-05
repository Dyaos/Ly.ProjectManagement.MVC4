using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.Data
{
    public class DbContextFactory
    {
        /// <summary>
        /// 公用的上下文对象（上下文是实例在线程内部唯一）
        /// </summary>
        /// <returns></returns>
        public static ProjectManagement_DBEntities GetCurrentDbContext()
        {
            ProjectManagement_DBEntities dbContext = CallContext.GetData("Ly_DbContext") as ProjectManagement_DBEntities;//在线程池了获取上下文
            if (dbContext == null)
            {
                dbContext = new ProjectManagement_DBEntities();
                CallContext.SetData("Ly_DbContext", dbContext);//将上下文保存在线程池里面
            }
            return dbContext;
        }
    }
}
