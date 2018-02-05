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
    public interface IItemsApp : IBaseApplication
    {
        void SubmitForm(SysItems entity, string keyVlaue);
        void DeleteForm(string keyVlaue);
    }
}
