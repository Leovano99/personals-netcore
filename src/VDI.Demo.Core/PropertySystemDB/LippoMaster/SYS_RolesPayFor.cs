using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("SYS_RolesPayFor")]
    public class SYS_RolesPayFor : AuditedEntity
    {
        public int entityID { get; set; }

        public int rolesID { get; set; }

        [ForeignKey("LK_PayFor")]
        public int payForID { get; set; }
        public virtual LK_PayFor LK_PayFor { get; set; }
    }
}
