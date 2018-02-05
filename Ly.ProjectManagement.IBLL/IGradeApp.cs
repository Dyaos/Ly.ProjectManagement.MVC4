using Ly.ProjectManagement.Data.Application;
using Ly.ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ly.ProjectManagement.IBLL
{
    public interface IGradeApp : IBaseApplication
    {
        void SubmitForm(Grade entity, string keyValue);
        void DeleteForm(string keyValue);
        void ChangedGradeStatus(string keyValue, bool type);
    }
}
