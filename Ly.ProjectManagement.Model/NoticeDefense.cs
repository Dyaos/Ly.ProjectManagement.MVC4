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
    
    public partial class NoticeDefense
    {
        public string noticeGuid { get; set; }
        public string teamGuid { get; set; }
        public string defemseRoomGuid { get; set; }
        public Nullable<System.DateTime> noticeTime { get; set; }
        public Nullable<System.DateTime> defenseTime { get; set; }
        public string remarks { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string createUserGuid { get; set; }
        public Nullable<System.DateTime> lastUpdateTime { get; set; }
        public string lastUpdateUserGuid { get; set; }
    
        public virtual DefemseRoom DefemseRoom { get; set; }
        public virtual ProjectTeam ProjectTeam { get; set; }
    }
}
