using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_UnitCode")]
    public class MS_UnitCode : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [StringLength(20)]
        public string unitCode { get; set; }

        [Required]
        [StringLength(50)]
        public string unitName { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
    }
}
