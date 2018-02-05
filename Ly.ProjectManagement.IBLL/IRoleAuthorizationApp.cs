using Ly.ProjectManagement.Data.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;

namespace Ly.ProjectManagement.IBLL
{
    public interface IRoleAuthorizationApp : IBaseApplication
    {
        /// <summary>
        /// 验证是否有权限操作该操作方法
        /// </summary>
        /// <returns></returns>
        bool ActionValidate(string roleGuid, string moduleGuid, string action);
        /// <summary>
        /// 获取当前用户的菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysModule> GetMenuList(string roleId);

        List<SysModuleButton> GetButtonList(string roleGuid);
    }
}
