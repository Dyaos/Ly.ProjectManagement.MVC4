using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.MVC4.Handler;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : BaseController
    {
        private IRoleApp roleApp;
        private IRoleAuthorizationApp roleAuthApp;
       public RoleController(IRoleApp roleApp, IRoleAuthorizationApp roleAuthApp)
        {
            this.roleApp = roleApp;
            this.roleAuthApp = roleAuthApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetRoleDataJson(Pagination pagination)
        {
            var data = new
            {
                rows = roleApp.FindList<Role>(r => r.isDel == false, pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = roleApp.FindEntity<Role>(r => r.roleGuid == keyValue);
            return Content(data.ToJson());
        }


        [HandlerAjaxOnly]
        public ActionResult SubmitForm(Role roleEntity, string permissionIds, string keyValue)
        {
            roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        public ActionResult DeleteForm(string keyValue)
        {
            roleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
