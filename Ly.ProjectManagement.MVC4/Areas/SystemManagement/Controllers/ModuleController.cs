using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.MVC4.Handler;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Domain.Enum;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleController : BaseController
    {
        private ISysModuleApp moduleApp;

        public ModuleController(ISysModuleApp moduleApp)
        {
            this.moduleApp = moduleApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = moduleApp.IQueryable<SysModule>(m => m.isDel == false).OrderBy(m => m.sortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.sysModuleName.Contains(keyword), "sysModuleGuid", "parentGuid");
            }
            var treeList = new List<TreeGridModel>();

            foreach (SysModule item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.parentGuid == item.sysModuleGuid) == 0 ? false : true;
                treeModel.id = item.sysModuleGuid;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentGuid;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectTreeJson(string keyValue)
        {
            var data = moduleApp.IQueryable<SysModule>(m => m.isDel == false && m.sysModuleGuid != keyValue && m.isMuen == false).ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysModule item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.sysModuleGuid;
                treeModel.text = item.sysModuleName;
                treeModel.parentId = item.parentGuid;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleApp.FindEntity<SysModule>(m => m.sysModuleGuid == keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysModule entity, string keyValue)
        {
            moduleApp.SubmitForm(entity, keyValue);
            if (string.IsNullOrEmpty(keyValue))
            {
                WirteOperationRecord("Module", DbLogType.Create, "增加菜单 - " + entity.ToJson());
            }
            else
            {
                WirteOperationRecord("Module", DbLogType.Update, "修改菜单 - " + entity.ToJson());
            }
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            moduleApp.DeleteForm(keyValue);
            WirteOperationRecord("Module", DbLogType.Delete, "删除菜单 - Guid" + keyValue);
            return Success("删除成功。");
        }
    }
}
