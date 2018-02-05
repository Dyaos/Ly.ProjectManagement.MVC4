using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Handler
{
    /// <summary>
    /// 约束是否为Ajax登录
    /// </summary>
    public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; set; }
        public HandlerAjaxOnlyAttribute(bool Ignore = false)
        {
            this.Ignore = Ignore;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (Ignore)
            {
                return true;
            }
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}