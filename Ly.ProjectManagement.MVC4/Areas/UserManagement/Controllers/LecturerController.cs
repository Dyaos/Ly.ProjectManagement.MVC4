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
    /// 讲师管理
    /// </summary>
    public class LecturerController : BaseController
    {
        private ITeacherApp teacherApp;
        public LecturerController(ITeacherApp teacherApp)
        {
            this.teacherApp = teacherApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var teacherData = teacherApp.FindList<Teacher>(t => t.isDel == false && t.teacherType == 2, pagination);
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
            try
            {
                teacherApp.DeleteForm(keyValue);
                WirteOperationRecord("Teacher", DbLogType.Delete, "guid - " + keyValue + " 删除成功");
                return Success("删除成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Teacher", DbLogType.Delete, "guid - " + keyValue + " 删除失败：" + ex.ToString());
                return Error("删除失败！");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Teacher entity, string keyValue)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.teacherType = 2;
                }
                teacherApp.SubmitForm(entity, keyValue);
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Teacher", DbLogType.Create, "guid - " + keyValue + " 增加成功");
                }
                else
                {
                    WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 更新成功");
                }
                return Success("操作成功！");
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    WirteOperationRecord("Teacher", DbLogType.Create, "guid - " + keyValue + " 增加失败：" + ex.ToString());
                }
                else
                {
                    WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 更新失败：" + ex.ToString());
                }
                return Error("操作失败！");
            }
        }

        [HttpPost]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = teacherApp.FindEntity<Teacher>(t => t.teacherGuid == keyValue);
            return Content(entity.ToJson());
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult ResetPassword(string keyValue, string userPassword)
        {
            try
            {
                teacherApp.ResetPassword(keyValue, userPassword);
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 重置密码");
                return Success("重置成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 重置密码失败，失败原因：" + ex.ToString());
                return Error("重置失败！");
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            try
            {
                teacherApp.ChangeEnableTeacher(keyValue, true);
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 启用账户成功");
                return Success("启用账户成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 启用账户失败，失败原因：" + ex.ToString());
                return Error("启用账户失败！");
            }

        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            try
            {
                teacherApp.ChangeEnableTeacher(keyValue, false);
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 禁用账户成功");
                return Success("禁用账户成功！");
            }
            catch (Exception ex)
            {
                WirteOperationRecord("Teacher", DbLogType.Update, "guid - " + keyValue + " 禁用账户失败，失败原因：" + ex.ToString());
                return Error("禁用账户失败！");
            }
        }
    }
}
