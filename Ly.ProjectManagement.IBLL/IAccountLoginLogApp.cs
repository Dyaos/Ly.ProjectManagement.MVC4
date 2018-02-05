using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.IBLL
{
    public interface IAccountLoginLogApp : IBaseApplication
    {
        List<AccountLoginLog> GetList(Pagination pagination, string queryJson);
    }
}
