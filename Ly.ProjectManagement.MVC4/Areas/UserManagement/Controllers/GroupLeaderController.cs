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

namespace Ly.ProjectManagement.MVC4.Areas.UserManagement.Controllers
{
    /// <summary>
    /// 组长管理
    /// </summary>
    public class GroupLeaderController : BaseController
    {
        private IGroupLeaderApp groupleadeApp;
        public GroupLeaderController(IGroupLeaderApp groupleadeApp)
        {
            this.groupleadeApp = groupleadeApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            //var groupleaderData=groupleadeApp.FindList<GroupLeader>(g = > g.isDel==false,pagination);
            var gldata = groupleadeApp.FindList<GroupLeader>(g => g.isDel == false, pagination);
            if (!string.IsNullOrEmpty(keyword))
            {
                gldata = gldata.Where(g => g.planGuid.Contains(keyword)).ToList();
            }

            var data = new
            {
                rows = groupleadeApp,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                groupleadeApp.DeleteForm(keyValue);
                WirteOperationRecord("GroupLeader", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("GroupLeader", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }


        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(GroupLeader entity, string keyValue)
        {
            try
            {
                groupleadeApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("GroupLeader", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("v", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功！");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("GroupLeader", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("GroupLeader", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败！");
            }
        }

        [HttpPost]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = groupleadeApp.FindEntity<GroupLeader>(t => t.leaderGuid == keyValue);
            return Content(entity.ToJson());
        }
    }
}
