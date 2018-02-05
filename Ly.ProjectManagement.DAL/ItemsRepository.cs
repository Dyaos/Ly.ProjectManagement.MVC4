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
    public class ItemsRepository : BaseRepository, IItemsRepository
    {
        public void DeleteForm(string keyVlaue)
        {
            var db = new BaseRepository().BeginTrans();
            var entity = db.FindEntity<SysItems>(t => t.itemGuid == keyVlaue);
            entity.delTime = DateTime.Now;
            entity.isDel = true;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            db.Update<SysItems>(entity);
            db.Commit();
        }

        public void SubmitForm(SysItems entity, string keyValue)
        {
            using (var db = new BaseRepository().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update<SysItems>(entity);
                }
                else
                {
                    db.Insert<SysItems>(entity);
                }

                db.Commit();
            }
        }
    }
}
