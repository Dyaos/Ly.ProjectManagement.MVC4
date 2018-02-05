using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;

namespace Ly.ProjectManagement.BLL
{
    public class ProjectPlanApp : BaseApplication, IProjectPlanApp
    {
        public ProjectPlanApp(IProjectPlanRepository repository)
        {
            CurrentRepository = repository;
        }

        void IProjectPlanApp.DeleteForm(string keyValue)
        {
            throw new NotImplementedException();
        }

        void IProjectPlanApp.SubmitForm(ProjectPlan entity, string keyValue)
        {
            ProjectPlan cloneEntity = entity;
            if (string.IsNullOrEmpty(keyValue))
            {
                cloneEntity.planGuid = Common.GuId();
                cloneEntity.createTime = DateTime.Now;
                cloneEntity.isDel = false;
                cloneEntity.isEnabled = true;
                cloneEntity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            else
            {
                entity = FindEntity<ProjectPlan>(p => p.planGuid == keyValue);
                entity.gradeGuid = cloneEntity.gradeGuid;
                entity.planStatus = cloneEntity.planStatus;
                entity.remarks = cloneEntity.remarks;
                entity.planBeginTime = cloneEntity.planBeginTime;
                entity.planEndTime = cloneEntity.planEndTime;
                entity.planType = cloneEntity.planType;
                entity.remarks = cloneEntity.remarks;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            (CurrentRepository as IProjectPlanRepository).SubmitForm(entity, keyValue);
        }
    }
}