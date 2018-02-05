using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.MVC4.Handler;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Domain.Enum;

namespace Ly.ProjectManagement.MVC4.Areas.ProjectManagement.Controllers
{
    /// <summary>
    /// 项目计划
    /// </summary>
    public class PlanController : BaseController
    {
        private IProjectPlanApp planApp;
        public PlanController(IProjectPlanApp planApp)
        {
            this.planApp = planApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ContentResult GetGridDataJson(Pagination pagination)
        {

            var data = new
            {
                rows = planApp.FindList<ProjectPlan>(r => r.isDel == false, pagination).Select(p => new { p.planGuid, p.planName, p.planStatus, p.remarks, p.planBeginTime, p.planEndTime, p.planType, p.createTime, p.Grade.gradeName }).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }


        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProjectPlan entity, string keyValue)
        {
            try
            {
                planApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("ProjectPlan", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("ProjectPlan", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("ProjectPlan", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("ProjectPlan", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
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
                planApp.DeleteForm(keyValue);
                WirteOperationRecord("ProjectPlan", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("ProjectPlan", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }
    }
}
