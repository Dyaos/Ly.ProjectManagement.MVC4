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
    /// 辅导员管理
    /// </summary>
    public class InstructorController : BaseController
    {
        private ITeacherApp teacherApp;
        public InstructorController(ITeacherApp teacherApp)
        {
            this.teacherApp = teacherApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var teacherData = teacherApp.FindList<Teacher>(t => t.isDel == false && t.teacherType == 1, pagination);
            if (!string.IsNullOrEmpty(keyword))
            {
                teacherData = teacherData.Where(t => t.jobNumber.Contains(keyword)).ToList();
            }
            var data = new
            {
                rows = teacherData,
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
            teacherApp.DeleteForm(keyValue);
            return Success("删除成功！");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Teacher entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.teacherType = 1;
            }
            teacherApp.SubmitForm(entity, keyValue);
            return Success("操作成功！");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = teacherApp.FindEntity<Teacher>(t => t.teacherGuid == keyValue);
            return Content(entity.ToJson());
        }

        public ActionResult ResetPassword(string keyValue, string pwd)
        {
            try
            {
                teacherApp.ResetPassword(keyValue, pwd);
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 重置密码");
                return Success("重置成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 重置密码失败，失败原因：" + ex.ToString());
                return Error("重置失败！");
            }
        }
    }
}
