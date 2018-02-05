using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.MVC4.Handler;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Domain.Enum;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LogonController : BaseController
    {

        private IAccountLoginLogApp logonApp;
        public LogonController(IAccountLoginLogApp logonApp) {
            this.logonApp = logonApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetLogonGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = logonApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = logonApp.FindEntity<AccountLoginLog>(a => a.logGuid == keyValue);
            return Content(entity.ToJson());
        }
    }
}
