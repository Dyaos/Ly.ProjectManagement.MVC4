using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    public class RoleAuthorizeController : BaseController
    {
        private ISysModuleApp moduleApp;
        private ISysModuleButtonApp btnApp;
        private IRoleAuthorizationApp authApp;
        public RoleAuthorizeController(ISysModuleApp moduleApp, ISysModuleButtonApp btnApp, IRoleAuthorizationApp authApp)
        {
            this.moduleApp = moduleApp;
            this.btnApp = btnApp;
            this.authApp = authApp;
        }


        public ActionResult GetPermissionTree(string roleId)
        {
            var moduleData = moduleApp.IQueryable<SysModule>(m => m.isDel == false).ToList();
            var btnData = btnApp.IQueryable<SysModuleButton>(m => m.isDel == false).ToList();
            var authData = new List<RoleAuthorization>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authData = authApp.IQueryable<RoleAuthorization>(a => a.authRoleGuid == roleId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModule item in moduleData)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduleData.Count(m => m.sysModuleGuid == m.parentGuid) == 0 ? false : true;
                tree.id = item.sysModuleGuid;
                tree.text = item.sysModuleName;
                tree.value = string.Empty;
                tree.parentId = item.parentGuid;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authData.Count(t => t.authModuleGuid == item.sysModuleGuid);
                tree.hasChildren = true;
                tree.img = item.sysModuleIcon == "" ? "" : item.sysModuleIcon;
                treeList.Add(tree);
            }
            foreach (SysModuleButton item in btnData)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = false;
                tree.id = item.sysBtnGuid;
                tree.text = item.sysBtnName;
                tree.value = item.sysEnCode;
                tree.parentId = item.sysModuleGuid;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authData.Count(t => t.authModuleGuid == item.sysBtnGuid);
                tree.hasChildren = hasChildren;
                tree.img = item.sysBtnIcon == "" ? "" : item.sysBtnIcon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
    }
}
