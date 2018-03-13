using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_AccountEmail")]
    public class MS_AccountEmail : AuditedEntity
    {
        public int entityID { get; set; }

        [StringLength(200)]
        public string emailTo { get; set; }

        [StringLength(100)]
        public string emailCc { get; set; }

        [ForeignKey("MS_Account")]
        public int accountID { get; set; }
        public virtual MS_Account MS_Account { get; set; }
    }
}
