using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    public class MS_UnitRoom : AuditedEntity
    {
        [ForeignKey("MS_UnitItem")]
        public int unitItemID { get; set; }
        public virtual MS_UnitItem MS_UnitItem { get; set; }

        [StringLength(50)]
        public string bedroom { get; set; }

        [StringLength(50)]
        public string bathroom { get; set; }
    }
}
