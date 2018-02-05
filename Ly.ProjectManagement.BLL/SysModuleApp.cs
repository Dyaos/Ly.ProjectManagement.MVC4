using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.IDAL;

namespace Ly.ProjectManagement.BLL
{
    public class SysModuleApp : BaseApplication, ISysModuleApp
    {
        public SysModuleApp(ISysModuleRepository repository)
        {
            this.CurrentRepository = repository;
        }
        public void DeleteForm(string keyValue)
        {
            (CurrentRepository as ISysModuleRepository).DeleteForm(keyValue);
        }

        public void SubmitForm(SysModule entity, string keyValue)
        {
            var cloneEntity = entity;
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.sysModuleGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                Insert<SysModule>(entity);
            }
            else
            {
                entity.sysModuleGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<SysModule>(entity);
            }
        }
    }
}