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
    /// 年级管理
    /// </summary>
    public class GradeController : BaseController
    {
        private IGradeApp gradeApp;
        public GradeController(IGradeApp gradeApp)
        {
            this.gradeApp = gradeApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var gradeData = gradeApp.FindList<Grade>(g => g.isDel == false, pagination);
            if (!string.IsNullOrEmpty(keyword))
            {
                gradeData = gradeData.Where(t => t.gradeName.Contains(keyword)).ToList();
            }
            var data = new
            {
                rows = gradeData,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectorJson()
        {
            var gradeData = gradeApp.IQueryable<Grade>(g => g.isDel == false && g.isEnabled == true).ToList();
            return Content(gradeData.ToJson());
        }


        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                gradeApp.DeleteForm(keyValue);
                WirteOperationRecord("Grade", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Grade", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Grade entity, string keyValue)
        {
            try
            {
                gradeApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Grade", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功！");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Grade", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败！");
            }
        }

        [HttpPost]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = gradeApp.FindEntity<Grade>(t => t.gradeGuid == keyValue);
            return Content(entity.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledGrade(string keyValue)
        {
            try
            {
                gradeApp.ChangedGradeStatus(keyValue, true);
                WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 启用年级成功");
                return Success("启用年级成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 启用年级失败，失败原因：" + ex.ToString());
                return Error("启用年级失败！");
            }

        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledGrade(string keyValue)
        {
            try
            {
                gradeApp.ChangedGradeStatus(keyValue, false);
                WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 禁用年级成功");
                return Success("禁用年级成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Grade", DbLogType.Update, "guid - " + keyValue + " 禁用年级失败，失败原因：" + ex.ToString());
                return Error("禁用年级失败！");
            }
        }

        public int Fun(int num)
        {
            if (num == 1 || num == 2)
            {
                return 1;
            }

            if (num <= 0)
            {
                return 0;
            }
            return Fun(num - 1) + Fun(num - 2);
        }
    }
}
