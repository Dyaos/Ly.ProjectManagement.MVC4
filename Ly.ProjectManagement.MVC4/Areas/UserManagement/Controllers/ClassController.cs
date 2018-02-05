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
    /// 班级管理
    /// </summary>
    public class ClassController : BaseController
    {
        private ISoftClassApp softApp;
        private IGradeApp gradeApp;
        public ClassController(ISoftClassApp softApp, IGradeApp gradeApp)
        {
            this.softApp = softApp;
            this.gradeApp = gradeApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            //.Select(s => new { s.remarks, s.softClassGuid, s.isEnabled, s.softClassName, s.createTime, s.Grade.gradeName })
            var gradeData = softApp.FindList<SoftClass>(s => s.isDel == false, pagination);
            if (!string.IsNullOrEmpty(keyword))
            {
                gradeData = gradeData.Where(s => s.softClassName.Contains(keyword)).ToList();
            }

            for (int i = 0; i < gradeData.Count(); i++)
            {
                var guid = gradeData[i].gradeGuid;
                gradeData[i].Grade = gradeApp.FindEntity<Grade>(g => g.gradeGuid == guid);
            }

            var resultData = gradeData.Select(s => new { s.remarks, s.softClassGuid, s.isEnabled, s.softClassName, s.createTime, s.Grade.gradeName });
            var data = new
            {
                rows = resultData,
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
                softApp.DeleteForm(keyValue);
                WirteOperationRecord("SoftClass", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("SoftClass", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SoftClass entity, string keyValue)
        {
            try
            {
                softApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("SoftClass", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("SoftClass", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功！");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("SoftClass", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("SoftClass", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败！");
            }
        }

        [HttpPost]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = softApp.FindEntity<SoftClass>(t => t.softClassGuid == keyValue);
            return Content(entity.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectorJson()
        {
            var classData = softApp.IQueryable<SoftClass>(g => g.isDel == false && g.isEnabled == true).ToList();
            return Content(classData.ToJson());
        }

    }
}
