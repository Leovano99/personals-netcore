using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("LK_Facing")]
    public class LK_Facing : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(3)]
        public string facingCode { get; set; }

        [Required]
        [StringLength(50)]
        public string facingName { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
    }
}
