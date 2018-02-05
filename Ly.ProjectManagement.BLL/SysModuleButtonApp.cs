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
    public class SysModuleButtonApp : BaseApplication, ISysModuleButtonApp
    {

        public SysModuleButtonApp(ISysModuleButtonRepository repository)
        {
            this.CurrentRepository = repository;
        }
        public void DeleteForm(string keyValue)
        {
            var entity = FindEntity<SysModuleButton>(m => m.sysBtnGuid == keyValue);
            entity.isDel = true;
            entity.delTime = DateTime.Now;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            Update<SysModuleButton>(entity);
        }

        public void SubmitForm(SysModuleButton entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.createTime = DateTime.Now;
                entity.sysBtnGuid = Common.GuId();
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                Insert<SysModuleButton>(entity);
            }
            else
            {
                entity.sysBtnGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<SysModuleButton>(entity);
            }
        }
    }
}