using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("SYS_RolesOthersType")]
    public class SYS_RolesOthersType : AuditedEntity
    {
        public int entityID { get; set; }

        public int rolesID { get; set; }

        [ForeignKey("LK_OthersType")]
        public int othersTypeID { get; set; }
        public virtual LK_OthersType LK_OthersType { get; set; }
    }
}
