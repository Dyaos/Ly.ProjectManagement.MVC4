using Ly.ProjectManagement.Data.Repository;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ly.ProjectManagement.IDAL
{
    public interface IRoleRepository : IBaseRepository
    {
        void SubmitForm(Role roleEntity,List<RoleAuthorization> roleAuthEntitys,string keyValue);
        void DeleteForm(string keyValue);
    }
}
