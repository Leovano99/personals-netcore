using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Zoning")]
    public class MS_Zoning : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(8)]
        public string zoningCode { get; set; }

        [Required]
        [StringLength(50)]
        public string zoningName { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }

    }
}
