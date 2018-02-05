using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Domain.Enum;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.MVC4.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 字典分类
    /// </summary>
    public class ItemTypeController : BaseController
    {
        private IItemsApp itemsApp;
        public ItemTypeController(IItemsApp itemsApp)
        {
            this.itemsApp = itemsApp;
        }



        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = itemsApp.IQueryable<SysItems>(t => t.isDel == false).ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysItems item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.itemGuid;
                treeModel.text = item.itemName;
                treeModel.parentId = item.parentGuid;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = itemsApp.IQueryable<SysItems>(t => t.isDel == false).ToList();
            var treeList = new List<TreeViewModel>();
            foreach (SysItems item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.parentGuid == item.itemGuid) == 0 ? false : true;
                tree.id = item.itemGuid;
                tree.text = item.itemName;
                tree.value = item.itemEnCode;
                tree.parentId = item.parentGuid;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ContentResult GetTreeGridJson()
        {
            var data = itemsApp.IQueryable<SysItems>(t => t.isDel == false).ToList();
            var treeList = new List<TreeGridModel>();
            foreach (SysItems item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.parentGuid == item.itemGuid) == 0 ? false : true;
                treeModel.id = item.itemGuid;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentGuid;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsApp.FindEntity<SysItems>(t => t.itemGuid == keyValue && t.isDel == false);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysItems entity, string keyValue)
        {
            try
            {
                itemsApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("SysItems", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("SysItems", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("SysItems", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("SysItems", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败。");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                itemsApp.DeleteForm(keyValue);
                WirteOperationRecord("SysItems", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("SysItems", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }
    }
}
