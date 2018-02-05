using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.MVC4.Handler;
using Ly.ProjectManagement.Domain.Enum;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 字典数据分类详情
    /// </summary>
    public class ItemDataController : BaseController
    {
        private IItemDetailsApp detailsApp;
        public ItemDataController(IItemDetailsApp detailsApp)
        {
            this.detailsApp = detailsApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ContentResult GetGridDataJson(string itemGuid, string keyword)
        {
            var data = detailsApp.GetItemDetailsList(itemGuid, keyword);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = detailsApp.FindEntity<SysItemDetails>(t => t.detailGuid == keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysItemDetails entity, string keyValue)
        {
            try
            {
                detailsApp.SubmitForm(entity, keyValue);
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
                detailsApp.DeleteForm(keyValue);
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
