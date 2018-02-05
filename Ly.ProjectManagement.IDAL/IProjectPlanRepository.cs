using Ly.ProjectManagement.Data.Repository;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.IDAL
{
    public interface IProjectPlanRepository : IBaseRepository
    {
        void SubmitForm(ProjectPlan entity, string keyValue);
        void DeleteForm(string keyValue);
    }
}
