using Ly.ProjectManagement.Data.Repository;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.IDAL
{
    public interface IItemsRepository : IBaseRepository
    {
        void SubmitForm(SysItems entity, string keyVlaue);
        void DeleteForm(string keyVlaue);
    }
}
