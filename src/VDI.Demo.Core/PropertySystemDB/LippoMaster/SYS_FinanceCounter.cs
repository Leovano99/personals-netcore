using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    public class SYS_FinanceCounter : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("MS_Account")]
        public int accID { get; set; }
        public virtual MS_Account MS_Account { get; set; }

        [Required]
        [StringLength(4)]
        public string year { get; set; }

        public long transNo { get; set; }

        public long TTBGNo { get; set; }

        public long ADJNo { get; set; }

        public long pmtBatchNo { get; set; }
    }
}
