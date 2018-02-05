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
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var db = new BaseRepository().BeginTrans();
            var roleEntity = db.FindEntity<Role>(r => r.roleGuid == keyValue);
            roleEntity.isDel = true;
            roleEntity.delTime = DateTime.Now;
            roleEntity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            db.Update<Role>(roleEntity);
            db.Delete<RoleAuthorization>(r => r.authRoleGuid == keyValue);
            db.Commit();
        }
        /// <summary>
        /// 增加或者更新
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthEntitys"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(Role roleEntity, List<RoleAuthorization> roleAuthEntitys, string keyValue)
        {
            using (var db = new BaseRepository().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update<Role>(roleEntity);
                }
                else
                {
                    db.Insert<Role>(roleEntity);
                }
                db.Delete<RoleAuthorization>(r => r.authRoleGuid == roleEntity.roleGuid);
                db.Insert<RoleAuthorization>(roleAuthEntitys);
                db.Commit();
            }
        }
    }
}
