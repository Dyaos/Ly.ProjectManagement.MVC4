using Ly.ProjectManagement.BLL;
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

namespace Ly.ProjectManagement.MVC4.Areas.UserManagement.Controllers
{
    /// <summary>
    /// 学生信息管理
    /// </summary>
    public class StudentInformationController : BaseController
    {
        private IStudentApp studentApp;
        public StudentInformationController(IStudentApp studentApp) {
            this.studentApp = studentApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var studentData = studentApp.FindList<Student>(t => t.isDel == false,pagination);
            if (!string.IsNullOrEmpty(keyword))
            {
                studentData = studentData.Where(t => t.stuNo.Contains(keyword)).ToList();
            }
            var data = new
            {
                rows = studentData,
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
                studentApp.DeleteForm(keyValue);
                WirteOperationRecord("Student", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Student", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }
        //zhe
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Student entity, string keyValue)
        {
            try
            {
                studentApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Student", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("Student", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功！");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Student", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("Student", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败！");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = studentApp.FindEntity<Student>(t => t.stuGuid == keyValue);
            return Content(entity.ToJson());
        }

        public ActionResult ResetPassword(string keyValue, string pwd)
        {
            try
            {
                studentApp.ResetPassword(keyValue, pwd);
                WirteOperationRecord("Student", DbLogType.Update, "guid - " + keyValue + " 重置密码");
                return Success("重置成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Student", DbLogType.Update, "guid - " + keyValue + " 重置密码失败，失败原因：" + ex.ToString());
                return Error("重置失败！");
            }
        }
    }
}
