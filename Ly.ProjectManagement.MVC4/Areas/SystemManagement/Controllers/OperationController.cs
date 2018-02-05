using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.MVC4.Handler;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement.Controllers
{
    /// <summary>
    /// 操作记录管理
    /// </summary>
    public class OperationController : BaseController
    {
        private IOperationRecordApp recordApp;
        public OperationController(IOperationRecordApp recordApp) {
            this.recordApp = recordApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetOperationRecordDataJson(Pagination pagination, string queryJson)
        {

            var data = new
            {
                rows = recordApp.GetList(pagination, queryJson),
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
            var entity = recordApp.FindEntity<OperationRecord>(a => a.operationGuid == keyValue);
            return Content(entity.ToJson());
        }
    }
}
