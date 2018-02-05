using Ly.ProjectManagement.Data.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;

namespace Ly.ProjectManagement.IBLL
{
    public interface ISysModuleButtonApp : IBaseApplication
    {
        void SubmitForm(SysModuleButton entity, string keyValue);

        void DeleteForm(string keyValue);
    }
}
