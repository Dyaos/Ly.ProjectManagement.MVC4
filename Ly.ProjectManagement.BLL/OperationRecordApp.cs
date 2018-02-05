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
    public class OperationRecordApp : BaseApplication, IOperationRecordApp
    {
        public OperationRecordApp(IOperationRecordRepository repository)
        {
            CurrentRepository = repository;
        }

        public List<OperationRecord> GetList(Pagination pagination, string queryJson)
        {
            var expre = ExtLinq.True<OperationRecord>();
            var param = queryJson.ToJObject();
            if (!param["keyword"].IsEmpty())
            {
                string type = param["keyword"].ToString();
                expre = expre.And(o => o.operationType.Contains(type));
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
                expre = expre.And(t => t.operationTime >= startTime && t.operationTime <= endTime);
            }
            return FindList<OperationRecord>(expre, pagination);
        }
  
    }
}
