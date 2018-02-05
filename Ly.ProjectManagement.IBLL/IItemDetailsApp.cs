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
    public interface IItemDetailsApp : IBaseApplication
    {
        List<SysItemDetails> GetItemDetailsList(string itemGuid,string keyword);
        void SubmitForm(SysItemDetails entity, string keyValue);
        void DeleteForm(string keyVlaue);
    }
}
