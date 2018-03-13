using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("SYS_RolesPayType")]
    public class SYS_RolesPayType : AuditedEntity
    {
        public int entityID { get; set; }

        public int rolesID { get; set; }

        [ForeignKey("LK_PayType")]
        public int payTypeID { get; set; }
        public virtual LK_PayType LK_PayType { get; set; }
    }
}
