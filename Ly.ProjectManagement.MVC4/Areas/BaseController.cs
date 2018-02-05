using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Domain.Enum;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.MVC4.Handler;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Areas
{
    [HandlerLogin]
    public class BaseController : Controller
    {
     
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Details()
        {
            return View();
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

        protected void WirteOperationRecord(string tableName, DbLogType type, string desc)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();

            OperationRecord entity = new OperationRecord();
            entity.operationGuid = Ly.ProjectManagement.Code.Common.GuId();
            entity.operatorGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            entity.operatorType = 1;
            entity.operationType = type.ToString();
            entity.operationTable = tableName;
            entity.operationDesc = desc;
            entity.operationTime = DateTime.Now;
            (ctx.GetObject("OperationRecordApp") as IOperationRecordApp).Insert<OperationRecord>(entity);
        }


    }
}