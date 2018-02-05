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
    public class SoftClassApp : BaseApplication, ISoftClassApp
    {
        public SoftClassApp(ISoftClassRepository repository)
        {
            this.CurrentRepository = repository;
        }


        public void SubmitForm(Model.SoftClass entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.softClassGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                entity.isEnabled = true;
                Insert<SoftClass>(entity);
            }
            else
            {
                entity.softClassGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<SoftClass>(entity);
            }
        }

        public void DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = FindEntity<SoftClass>(s => s.softClassGuid == keyValue);
                entity.isDel = true;
                entity.delTime = DateTime.Now;
                entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<SoftClass>(entity);
            }

        }
    }
}