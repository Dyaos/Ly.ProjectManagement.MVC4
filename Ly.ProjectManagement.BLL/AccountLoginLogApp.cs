using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.IDAL;

namespace Ly.ProjectManagement.BLL
{
    public class AccountLoginLogApp : BaseApplication, IAccountLoginLogApp
    {
        public AccountLoginLogApp(IAccountLoginLogRepository repository)
        {
            CurrentRepository = repository;
        }
        public List<AccountLoginLog> GetList(Pagination pagination, string queryJson)
        {
            var expre = ExtLinq.True<AccountLoginLog>();
            var param = queryJson.ToJObject();
            if (!param["keyword"].IsEmpty())
            {
                string ip = param["keyword"].ToString();
                expre = expre.And(o => o.loginIpAddress.Contains(ip));
            }
            if (!param["timeType"].IsEmpty())
            {
                string timeType = param["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3);
                        break;
                    default:
                        break;
                }
                expre = expre.And(t => t.loginTime >= startTime && t.loginTime <= endTime);
            }
            return FindList<AccountLoginLog>(expre, pagination);
        }

    }
}
