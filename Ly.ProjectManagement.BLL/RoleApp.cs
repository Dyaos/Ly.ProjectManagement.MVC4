using Ly.ProjectManagement.Code;
using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using Ly.ProjectManagement.IDAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.BLL
{
    public class RoleApp : BaseApplication, IRoleApp
    {

        public RoleApp(IRoleRepository repository)
        {
            CurrentRepository = repository;
        }

        public void DeleteForm(string keyValue)
        {
            (CurrentRepository as IRoleRepository).DeleteForm(keyValue);
        }


        public void SubmitForm(Role roleEntity, string[] permissionIds, string keyValue)
        {
            Role cloneRoleEntity = roleEntity;
            if (string.IsNullOrEmpty(keyValue))
            {
                roleEntity.roleGuid = Common.GuId();
                roleEntity.createTime = DateTime.Now;
                roleEntity.isDel = false;
                roleEntity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }
            else
            {
                roleEntity = FindEntity<Role>(r => r.roleGuid == keyValue);
                roleEntity.roleLv = cloneRoleEntity.roleLv;
                roleEntity.roleName = cloneRoleEntity.roleName;
                roleEntity.remarks = cloneRoleEntity.remarks;
                roleEntity.sortCode = cloneRoleEntity.sortCode;
                roleEntity.lastUpdateTime = DateTime.Now;
                roleEntity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
            }

            var moduleData = IQueryable<SysModule>().ToList();
            var buttonData = IQueryable<SysModuleButton>().ToList();
            List<RoleAuthorization> list = new List<RoleAuthorization>();
            int id = 0;
            foreach (var itemId in permissionIds)
            {
                id++;
                RoleAuthorization roleAuthEntity = new RoleAuthorization();
                roleAuthEntity.authGuid = Common.GuId();
                roleAuthEntity.authRoleGuid = roleEntity.roleGuid;
                roleAuthEntity.sortCode = id;
                roleAuthEntity.authModuleGuid = itemId;
                roleAuthEntity.createTime = DateTime.Now;
                roleAuthEntity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                if (moduleData.Find(m => m.sysModuleGuid == itemId) != null)
                {
                    roleAuthEntity.authModuleType = 1;
                }
                else if (buttonData.Find(m => m.sysBtnGuid == itemId) != null)
                {
                    roleAuthEntity.authModuleType = 2;
                }
                list.Add(roleAuthEntity);
            }
           (CurrentRepository as IRoleRepository).SubmitForm(roleEntity, list, keyValue);
        }

    }
}