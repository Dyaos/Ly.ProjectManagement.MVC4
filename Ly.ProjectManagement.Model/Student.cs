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
    
    public partial class Student
    {
        public string stuGuid { get; set; }
        public string roleGuid { get; set; }
        public string softClassGuid { get; set; }
        public string stuNo { get; set; }
        public string stuPwd { get; set; }
        public string stuName { get; set; }
        public Nullable<bool> stuSex { get; set; }
        public string stuNation { get; set; }
        public string stuBirthday { get; set; }
        public string stuCard { get; set; }
        public string stuPlace { get; set; }
        public string stuQQ { get; set; }
        public string stuWeChat { get; set; }
        public string stuPhone { get; set; }
        public Nullable<bool> isDel { get; set; }
        public string remarks { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string createUserGuid { get; set; }
        public Nullable<System.DateTime> lastUpdateTime { get; set; }
        public string lastUpdateUserGuid { get; set; }
        public Nullable<System.DateTime> delTime { get; set; }
        public string delUserGuid { get; set; }
        public Nullable<bool> isEnabled { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual SoftClass SoftClass { get; set; }
    }
}