using Ly.ProjectManagement.Data.Repository;
using Ly.ProjectManagement.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Code;

namespace Ly.ProjectManagement.DAL
{
    public class ProjectPlanRepository : BaseRepository, IProjectPlanRepository
    {
        public void DeleteForm(string keyValue)
        {
            var db = new BaseRepository().BeginTrans();
            var entity = db.FindEntity<ProjectPlan>(p => p.planGuid == keyValue);
            entity.isDel = true;
            entity.delTime = DateTime.Now;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            db.Update<ProjectPlan>(entity);
            db.Commit();
        }

        public void SubmitForm(ProjectPlan entity, string keyValue)
        {
            using (var db = new BaseRepository().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update<ProjectPlan>(entity);
                }
                else
                {
                    db.Insert<ProjectPlan>(entity);
                }
                db.Commit();
            }
        }
    }
}
