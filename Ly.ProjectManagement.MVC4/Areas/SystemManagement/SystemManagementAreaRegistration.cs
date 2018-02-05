using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ly.ProjectManagement.MVC4.Areas.SystemManagement
{
    public class SystemManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "SystemManagement"; } }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
             this.AreaName + "_Default",
             this.AreaName + "/{controller}/{action}/{id}",
             new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
             new string[] { "Ly.ProjectManagement.MVC4.Areas." + this.AreaName + ".Controllers" }
             );
        }
    }
}