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
    public class TeacherApp : BaseApplication, ITeacherApp
    {
        public TeacherApp(ITeacherRepository repository)
        {
            this.CurrentRepository = repository;
        }
        public Teacher CheckLogin(string name, string pwd)
        {
            Teacher Teacher = FindEntity<Teacher>(t => t.jobNumber == name);
            if (Teacher != null)
            {
                if (Teacher.isEnabled == true)
                {
                    if (pwd == Teacher.teacherPwd)
                    {
                        return Teacher;
                    }
                    else
                    {
                        throw new Exception("密码错误，请重新输入！");
                    }
                }
                else
                {
                    throw new Exception("该账户被系统锁定，请联系管理员！");
                }
            }
            else
            {
                throw new Exception("该账户不存在，请重新输入！");
            }
        }

        public void DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = FindEntity<Teacher>(t => t.teacherGuid == keyValue);
                entity.isDel = true;
                entity.delTime = DateTime.Now;
                entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<Teacher>(entity);
            }
        }

        public void SubmitForm(Teacher entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.teacherGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                entity.isEnabled = true;
                entity.teacherPwd = Md5.md5(entity.teacherPwd, 32);
                Insert<Teacher>(entity);
            }
            else
            {
                entity.teacherGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<Teacher>(entity);
            }
        }

        public void ResetPassword(string keyValue, string pwd)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = new Teacher() { teacherGuid = keyValue, teacherPwd = Md5.md5(pwd, 32) };
                Update<Teacher>(entity);
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