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
    public class R_Teacher_SoftClassApp : BaseApplication, IR_Teacher_SoftClassApp
    {
        public R_Teacher_SoftClassApp(IR_Teacher_SoftClassRepository repository)
        {
            CurrentRepository = repository;
        }
    }
}