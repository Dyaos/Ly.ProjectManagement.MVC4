//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ly.ProjectManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectTest
    {
        public string proTestGuid { get; set; }
        public string proModuleGuid { get; set; }
        public string proMuduleGuid { get; set; }
        public Nullable<int> testStatus { get; set; }
        public string testGuid { get; set; }
        public Nullable<System.DateTime> testTime { get; set; }
        public string testPresentation { get; set; }
        public Nullable<bool> isDel { get; set; }
        public string remarks { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string createUserGuid { get; set; }
        public Nullable<System.DateTime> lastUpdateTime { get; set; }
        public string lastUpdateUserGuid { get; set; }
        public Nullable<System.DateTime> delTime { get; set; }
        public string delUserGuid { get; set; }
        public Nullable<bool> isEnabled { get; set; }
    
        public virtual ProjectModule ProjectModule { get; set; }
    }
}
