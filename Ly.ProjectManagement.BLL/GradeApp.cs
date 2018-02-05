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
    public class GradeApp : BaseApplication, IGradeApp
    {
        public GradeApp(IGradeRepository repository)
        {
            CurrentRepository = repository;
        }
        public void ChangedGradeStatus(string keyValue, bool type)
        {
            var entity = new Grade();
            entity.gradeGuid = keyValue;
            entity.isEnabled = type;
            entity.lastUpdateTime = DateTime.Now;
            entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            Update<Grade>(entity);
        }

        public void DeleteForm(string keyValue)
        {
            var entity = new Grade();
            entity.gradeGuid = keyValue;
            entity.isDel = true;
            entity.delTime = DateTime.Now;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            Update<Grade>(entity);
        }

        public void SubmitForm(Grade entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.gradeGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                entity.isEnabled = true;
                Insert<Grade>(entity);
            }
            else
            {
                entity.gradeGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<Grade>(entity);
            }
        }
    }
}