using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.Model;
using System.Text;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;

namespace Ly.ProjectManagement.MVC4.Controllers
{
    public class ClientDataController : Controller
    {

        private IRoleAuthorizationApp roleAuthApp;

        public ClientDataController(IRoleAuthorizationApp roleAuthApp) {
            this.roleAuthApp = roleAuthApp;
        }

        // GET: /ClientData/
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                authorizeMenu = GetMenuList(),
                authorizeButton = GetButtonList()
            };
            return Content(data.ToJson());
        }

        private object GetButtonList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleGuid;
            var data = roleAuthApp.GetButtonList(roleId);
            var dataModuleId = data.Distinct(new ExtList<SysModuleButton>("sysModuleGuid"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (SysModuleButton item in dataModuleId)
            {
                var buttonList = data.Where(t => t.sysModuleGuid.Equals(item.sysModuleGuid));
                dictionary.Add(item.sysModuleGuid, buttonList);
            }
            return dictionary;
        }

        private object GetMenuList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleGuid;
            return ToMenuJson(roleAuthApp.GetMenuList(roleId), "0");
        }
        private string ToMenuJson(List<SysModule> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<SysModule> entitys = data.FindAll(t => t.parentGuid == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.sysModuleGuid) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }


    }
}
