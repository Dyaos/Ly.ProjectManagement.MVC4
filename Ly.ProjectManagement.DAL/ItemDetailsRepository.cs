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
    public class ItemDetailsRepository : BaseRepository, IItemDetailsRepository
    {
        public void DeleteForm(string keyVlaue)
        {
            var db = new BaseRepository().BeginTrans();
            var entity = db.FindEntity<SysItemDetails>(t => t.detailGuid == keyVlaue);
            entity.delTime = DateTime.Now;
            entity.isDel = true;
            entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            db.Update<SysItemDetails>(entity);
            db.Commit();
        }

        public void SubmitForm(SysItemDetails entity, string keyValue)
        {
            using (var db = new BaseRepository().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update<SysItemDetails>(entity);
                }
                else
                {
                    db.Insert<SysItemDetails>(entity);
                }

                db.Commit();
            }
        }
    }
}
