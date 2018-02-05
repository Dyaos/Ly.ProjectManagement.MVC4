using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.IBLL
{
    public interface IStudentApp : IBaseApplication
    {
        Student CheckLogin(string name, string pwd);
        void SubmitForm(Student entity, string keyValue);

        void DeleteForm(string keyValue);

        void ResetPassword(string keyValue, string pwd);

        void ChangeEnableTeacher(string keyValue, bool state);
    }
}
