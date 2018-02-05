using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.BLL;
using Spring.Context;
using Spring.Context.Support;
using Ly.ProjectManagement.IBLL;

namespace Ly.ProjectManagement.MVC4.Handler
{
    /// <summary>
    /// 操作方法筛选器
    /// 验证用户是否有权限访问当前操作方法
    /// </summary>
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; set; }
        public HandlerAuthorizeAttribute(bool Ignore = false)
        {
            this.Ignore = Ignore;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (OperatorProvider.Provider.GetCurrent() != null && OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                return;
            }
            if (Ignore)
            {
                return;
            }
            if (!this.ActionAuthorize(filterContext))
            {
                StringBuilder strScript = new StringBuilder();
                strScript.Append("<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');</script>");
                filterContext.Result = new ContentResult() { Content = strScript.ToString() };
            }
        }

        /// <summary>
        /// 验证当前用户是否有权限访问目标的操作方法
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ActionAuthorize(ActionExecutingContext filterContext)
        {
            var operatorProvider = OperatorProvider.Provider.GetCurrent();//获取当前用户
            if (operatorProvider == null)
            {
                filterContext.HttpContext.Response.Write("<script>top.location='/Login/Index'</script>");
                return false;
            }
            IApplicationContext ctx = ContextRegistry.GetContext();
            var roleGuid = operatorProvider.RoleGuid;//获取当前角色Guid
            var moduleGuid = WebHelper.GetCookie("ly_currentModuleGuid");
            var action = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            var roleAuthApp = ctx.GetObject("RoleAuthApp") as IRoleAuthorizationApp;
            return roleAuthApp.ActionValidate(roleGuid, moduleGuid, action);
        }
    }
}