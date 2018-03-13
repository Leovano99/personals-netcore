using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_UnitTaskList")]
    public class MS_UnitTaskList : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [Required]
        public string dataUnit { get; set; }
    }
}
