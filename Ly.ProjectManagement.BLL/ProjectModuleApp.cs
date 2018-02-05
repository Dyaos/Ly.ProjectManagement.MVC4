using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.BLL
{
    public class ProjectModuleApp : BaseApplication, IProjectModuleApp
    {
        public ProjectModuleApp(IProjectModuleRepository repository)
        {
            CurrentRepository = repository;
        }
    }
}