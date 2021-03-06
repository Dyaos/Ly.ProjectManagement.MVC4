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
    
    public partial class DefemseRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DefemseRoom()
        {
            this.NoticeDefense = new HashSet<NoticeDefense>();
        }
    
        public string defemseRoomGuid { get; set; }
        public string defemseTeacherGuid { get; set; }
        public string instructorGuid { get; set; }
        public string address { get; set; }
        public string remarks { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string createUserGuid { get; set; }
        public Nullable<System.DateTime> lastUpdateTime { get; set; }
        public string lastUpdateUserGuid { get; set; }
        public Nullable<System.DateTime> delTime { get; set; }
        public string delUserGuid { get; set; }
        public string planGuid { get; set; }
    
        public virtual ProjectPlan ProjectPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeDefense> NoticeDefense { get; set; }
    }
}
