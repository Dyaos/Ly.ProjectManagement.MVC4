using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.MVC4.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ly.ProjectManagement.MVC4.Controllers
{                  
    [HandlerLogin]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}
