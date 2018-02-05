using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Data.Repository;
using Ly.ProjectManagement.IDAL;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.DAL
{
    public class SysModuleRepository : BaseRepository, ISysModuleRepository
    {
        public void DeleteForm(string keyValue)
        {
            var db = new BaseRepository().BeginTrans();
            var entity = FindEntity<SysModule>(m => m.sysModuleGuid == keyValue);
            entity.isDel = true;
            entity.delTime = DateTime.Now;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            db.Update<SysModule>(entity);
            if (db.IQueryable<SysModule>().Count(m => m.parentGuid.Equals(keyValue)) > 0)
            {
                var childEntity = IQueryable<SysModule>(m => m.parentGuid == keyValue);
                foreach (var item in childEntity)
                {
                    item.isDel = true;
                    item.delTime = DateTime.Now;
                    item.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                    db.Update<SysModule>(item);
                }
            }
            else
            {
                var childBtn = IQueryable<SysModuleButton>(mb => mb.sysModuleGuid == entity.sysModuleGuid);
                if (childBtn != null)
                {
                    foreach (var item in childBtn)
                    {
                        item.isDel = true;
                        item.delTime = DateTime.Now;
                        item.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                        db.Update<SysModuleButton>(item);
                    }
                }
            }
            db.Commit();
        }
    }
}
