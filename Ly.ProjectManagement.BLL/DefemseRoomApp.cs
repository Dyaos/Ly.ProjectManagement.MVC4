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
    public class DefemseRoomApp : BaseApplication, IDefemseRoomApp
    {
        public DefemseRoomApp(IDefemseRoomRepository repository)
        {
            CurrentRepository = repository;
        }
    }
}