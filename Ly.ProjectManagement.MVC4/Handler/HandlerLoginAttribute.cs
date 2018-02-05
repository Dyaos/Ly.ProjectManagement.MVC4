using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Handler
{
    /// <summary>
    /// 验证用户是否登陆
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; set; }
        public HandlerLoginAttribute(bool Ignore = false)
        {
            this.Ignore = Ignore;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore)
            {
                return;
            }
            if (OperatorProvider.Provider.GetCurrent() == null)
            {
               
                filterContext.HttpContext.Response.Write("<script>top.location='/Login/Index'</script>");
                return;
            }
        }
    }
}