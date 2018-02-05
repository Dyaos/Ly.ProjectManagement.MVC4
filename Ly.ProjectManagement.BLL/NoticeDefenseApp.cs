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
    public class NoticeDefenseApp : BaseApplication, INoticeDefenseApp
    {
        public NoticeDefenseApp(INoticeDefenseRepository repository)
        {
            CurrentRepository = repository;
        }
    }
}