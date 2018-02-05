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
    public class GroupLeaderApp : BaseApplication, IGroupLeaderApp
    {

        public GroupLeaderApp(IGroupLeaderRepository repository)
        {
            CurrentRepository = repository;
        }

        public void DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = FindEntity<GroupLeader>(g => g.leaderGuid == keyValue);
                entity.isDel = true;
                entity.delTime = DateTime.Now;
                entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<GroupLeader>(entity);
            }
        }
        public void SubmitForm(GroupLeader entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.leaderGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                entity.isEnabled = true;
                Insert<GroupLeader>(entity);
            }
            else
            {
                entity.leaderGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<GroupLeader>(entity);
            }
        }
        public void ChangeEnableTeacher(string keyValue, bool state)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = new Teacher() { teacherGuid = keyValue, isEnabled = state };
                Update<Teacher>(entity);
            }
        }
    }
}