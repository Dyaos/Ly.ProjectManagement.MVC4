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
    public class StudentApp : BaseApplication, IStudentApp
    {


        public StudentApp(IStudentRepository repository)
        {
            this.CurrentRepository = repository;
        }

        public Student CheckLogin(string name, string pwd)
        {
            Student student = FindEntity<Student>(s => s.stuNo == name);
            if (student != null)
            {
                if (student.isEnabled == true)
                {
                    if (pwd == student.stuPwd)
                    {
                        return student;
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
                var entity = FindEntity<Student>(t => t.stuGuid == keyValue);
                entity.isDel = true;
                entity.delTime = DateTime.Now;
                entity.delUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<Student>(entity);
            }
        }

        public void SubmitForm(Student entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.stuGuid = Common.GuId();
                entity.createTime = DateTime.Now;
                entity.createUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                entity.isDel = false;
                entity.isEnabled = true;
                entity.stuPwd = Md5.md5(entity.stuPwd, 32);
                Insert<Student>(entity);
            }
            else
            {
                entity.stuGuid = keyValue;
                entity.lastUpdateTime = DateTime.Now;
                entity.lastUpdateUserGuid = OperatorProvider.Provider.GetCurrent().UserGuid;
                Update<Student>(entity);
            }
        }

        public void ResetPassword(string keyValue, string pwd)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = new Student() { stuGuid = keyValue, stuPwd = Md5.md5(pwd, 32) };
                Update<Student>(entity);
            }
        }

        public void ChangeEnableTeacher(string keyValue, bool state)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entity = new Student() { stuGuid = keyValue, isEnabled = state };
                Update<Student>(entity);
            }
        }
    }
}