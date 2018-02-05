using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.MVC4.Handler;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Domain.Enum;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 按钮管理
    /// </summary>
    public class ButtonController : BaseController
    {

        private ISysModuleButtonApp btnApp;
        private ISysModuleApp moduleApp;
        public ButtonController(ISysModuleButtonApp btnApp, ISysModuleApp moduleApp)
        {
            this.btnApp = btnApp;
            this.moduleApp = moduleApp;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysModuleButton entity, string keyValue)
        {
            btnApp.SubmitForm(entity, keyValue);
            if (string.IsNullOrEmpty(keyValue))
            {
                WirteOperationRecord("Button", DbLogType.Create, "增加按钮 - " + entity.ToJson());
            }
            else
            {
                WirteOperationRecord("Button", DbLogType.Update, "修改按钮 - " + entity.ToJson());
            }
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            btnApp.DeleteForm(keyValue);
            WirteOperationRecord("Button", DbLogType.Delete, "删除按钮 - Guid" + keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = btnApp.FindEntity<SysModuleButton>(m => m.sysBtnGuid == keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectTreeJson(string keyValue)
        {
            var data = moduleApp.IQueryable<SysModule>(m => m.isDel == false && m.sysModuleGuid != keyValue && m.sysModuleLv != 1).ToList();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = btnApp.IQueryable<SysModuleButton>(m => m.isDel == false).OrderBy(m => m.sortCode).ToList();
            var moduleData = moduleApp.IQueryable<SysModule>(m => m.isDel == false && m.sysModuleLv != 1).ToList();
            foreach (var item in moduleData)
            {
                SysModuleButton entity = new SysModuleButton();
                entity.sysBtnGuid = item.sysModuleGuid;
                entity.sysBtnName = item.sysModuleName;
                entity.sysModuleGuid = "0";
                entity.sortCode = 1;
                entity.remarks = item.remarks;
                data.Add(entity);
            }

            var treeList = new List<TreeGridModel>();
            foreach (SysModuleButton item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(m => m.sysModuleGuid == item.sysBtnGuid) > 0;
                treeModel.id = item.sysBtnGuid;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.sysModuleGuid;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
    }
}
