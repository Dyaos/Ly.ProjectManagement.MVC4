using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.Domain.ViewModel;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.IDAL;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.BLL
{
    public class RoleAuthorizationApp : BaseApplication, IRoleAuthorizationApp
    {
        private ISysModuleApp sysModuleApp;
        private ISysModuleButtonApp sysModuleButtonApp;
        private IRoleApp roleApp;

        public RoleAuthorizationApp(ISysModuleApp sysModuleApp, ISysModuleButtonApp sysModuleButtonApp, IRoleApp roleApp, IRoleAuthorizationRepository repository)
        {
            this.sysModuleApp = sysModuleApp;
            this.sysModuleButtonApp = sysModuleButtonApp;
            this.roleApp = roleApp;
            this.CurrentRepository = repository;
        }


        /// <summary>
        /// 角色授权功能验证
        /// </summary>
        /// <param name="roleGuid">当前角色Huid</param>
        /// <param name="moduleGuid">当前模块Guid</param>
        /// <param name="action">操作方法</param>
        /// <returns>返回是否具有该操作方法的权限 </returns>
        public bool ActionValidate(string roleGuid, string moduleGuid, string action)
        {
            var authUrlData = new List<AuthorizeActionModel>();//当前角色的功能
            var role = roleApp.FindEntity<Role>(r => r.roleGuid == roleGuid);
            var cacheAuthUrlData = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authurldata_" + role.roleGuid);//读取缓存
            if (cacheAuthUrlData == null)
            {
                var moduleData = sysModuleApp.IQueryable<SysModule>().ToList();
                var moduleBtnData = sysModuleButtonApp.IQueryable<SysModuleButton>().ToList();
                var roleAuthData = IQueryable<RoleAuthorization>(r => r.authRoleGuid == role.roleGuid).ToList();//查询当前角色的功能
                foreach (var item in roleAuthData)
                {
                    if (item.authModuleType == 1)
                    {
                        SysModule sysModule = moduleData.Find(m => m.sysModuleGuid == item.authModuleGuid);
                        authUrlData.Add(new AuthorizeActionModel() { moduleGuid = sysModule.sysModuleGuid, moduleUrlAddress = sysModule.sysModuleUri });
                    }
                    else if (item.authModuleType == 2)
                    {
                        SysModuleButton moduleButton = moduleBtnData.Find(b => b.sysBtnGuid == item.authModuleGuid);
                        authUrlData.Add(new AuthorizeActionModel() { moduleGuid = moduleButton.sysBtnGuid, moduleUrlAddress = moduleButton.sysBtnUrlAddress });
                    }
                }
                CacheFactory.Cache().WriteCache(authUrlData, "authurldata_" + roleGuid, DateTime.Now.AddMinutes(5));//写入缓存
            }
            else
            {
                authUrlData = cacheAuthUrlData;
            }
            authUrlData = authUrlData.FindAll(m => m.moduleGuid.Equals(moduleGuid));
            foreach (var item in authUrlData)
            {
                if (!string.IsNullOrEmpty(item.moduleUrlAddress))
                {
                    string[] url = item.moduleUrlAddress.Split('?');
                    if (item.moduleGuid == moduleGuid && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前用户的菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysModule> GetMenuList(string roleGuid)
        {
            var data = new List<SysModule>();
            var role = roleApp.FindEntity<Role>(r => r.roleGuid == roleGuid);
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = sysModuleApp.IQueryable<SysModule>(m => m.isDel == false).ToList();
            }
            else
            {
                var moduleData = sysModuleApp.IQueryable<SysModule>(m => m.isDel == false).ToList();
                var authData = IQueryable<RoleAuthorization>(r => r.authRoleGuid == role.roleGuid && r.authModuleType == 1).ToList();
                foreach (var item in authData)
                {
                    SysModule moduleEntity = moduleData.Find(m => m.sysModuleGuid == item.authModuleGuid);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.OrderBy(m => m.sortCode).ToList(); ;
        }

        /// <summary>
        /// 获取认证按钮
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public List<SysModuleButton> GetButtonList(string roleGuid)
        {
            var btnData = new List<SysModuleButton>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                btnData = sysModuleButtonApp.IQueryable<SysModuleButton>().ToList();
            }
            else
            {
                var buttonData = sysModuleButtonApp.IQueryable<SysModuleButton>().ToList();
                var authButtonData = IQueryable<RoleAuthorization>(r => r.authRoleGuid == roleGuid && r.authModuleType == 2).ToList();
                foreach (var item in authButtonData)
                {
                    SysModuleButton entity = buttonData.Find(b => b.sysBtnGuid == item.authModuleGuid);
                    if (entity != null)
                    {
                        btnData.Add(entity);
                    }
                }
            }
            return btnData;
        }
    }


}